using dotnet_api.Data;
using dotnet_api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Net;
using System.Security.Claims;
using System.Threading.RateLimiting;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddMvc();
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
    MariaDbServerVersion.Create(new Version(10,0,6), ServerType.MariaDb)));
// identity
builder.Services.AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<ApiDbContext>();

string secretkey = builder.Configuration.GetValue<string>("SecretKey")
    ?? throw new InvalidOperationException("SecretKey manquante (user-secrets en dev, variable d'environnement en prod)");

if (Encoding.UTF8.GetByteCount(secretkey) < 32)
    throw new InvalidOperationException("SecretKey trop courte (32 octets minimum, 64 recommandés)");

// authentification JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true; // a changer si https
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey)),
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
        // révocation: le jeton porte le security stamp de l'utilisateur (changé au logout / changement de mot de passe)
        // ponytail: une lecture en base par requête authentifiée; mettre en cache si le trafic augmente
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var users = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                var user = await users.GetUserAsync(context.Principal!);
                var stamp = context.Principal!.FindFirstValue(users.Options.ClaimsIdentity.SecurityStampClaimType);
                if (user == null || user.SecurityStamp != stamp)
                    context.Fail("Jeton révoqué");
            }
        };
    });

// limite le débit de login/inscription par IP (anti force brute)
// ponytail: IP directe; derrière un proxy, activer ForwardedHeaders pour ne pas limiter tout le monde ensemble
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("auth", context => RateLimitPartition.GetFixedWindowLimiter(
        context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 10, Window = TimeSpan.FromMinutes(1) }));
});

// Register ProblemDetails service
builder.Services.AddProblemDetails();

// gestion des routes SPA ( pour angular )
builder.Services.AddSpaStaticFiles(configuration =>
    configuration.RootPath = "wwwroot"
);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
    app.UseHsts(); // 30 jours par défaut; ajouter HstsOptions (MaxAge, IncludeSubDomains) une fois HTTPS validé partout

app.UseHttpsRedirection();

// en-têtes de sécurité sur toutes les réponses
// ponytail: pas de CSP complète (script-src/style-src) car le build Angular inline du CSS critique; à ajouter avec nonces si besoin
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers["X-Content-Type-Options"] = "nosniff";
    headers["X-Frame-Options"] = "DENY";
    headers["Referrer-Policy"] = "no-referrer";
    headers["Content-Security-Policy"] = "frame-ancestors 'none'";
    await next();
});

app.UseCors(options => options
    .WithOrigins(app.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? [])
    .AllowAnyMethod().AllowAnyHeader());
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// active angular (en dernier : fallback vers index.html)
app.UseSpaStaticFiles();
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";
});

app.Run();
