using dotnet_api.Data;
using dotnet_api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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

string secretkey = builder.Configuration.GetValue<string>("SecretKey");

// authentification JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true; // a changer si https
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretkey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Register ProblemDetails service
builder.Services.AddProblemDetails();

// gestion des routes SPA ( pour angular )
//builder.Services.AddSpaStaticFiles(configuration =>
//    configuration.RootPath = "wwwroot"
//);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// active angular

//app.UseSpaStaticFiles();
//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "wwwroot";
//});



// active l'authentification
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
