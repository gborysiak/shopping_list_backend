using dotnet_api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class PartController : ControllerBase
    {
        public readonly ApiDbContext _apiDbContext;
        public PartController(ApiDbContext apiDbContext)
        {
            this._apiDbContext = apiDbContext;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // pièces de l'utilisateur connecté uniquement
        private IQueryable<Models.Part> MyParts =>
            this._apiDbContext.Parts.Where(p => p.OwnerId == UserId);

        // la catégorie référencée doit appartenir à l'utilisateur
        private bool OwnsCategory(int categoryId) =>
            this._apiDbContext.Categories.Any(c => c.id == categoryId && c.OwnerId == UserId);

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(MyParts.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var part = MyParts.FirstOrDefault(p => p.id == id);
            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        [HttpPost]
        public IActionResult CreatePart([FromBody] Models.Part part)
        {
            if (!OwnsCategory(part.categoryId))
            {
                return BadRequest();
            }

            part.id = 0;
            part.OwnerId = UserId;
            part.dateCreated = DateTime.Now;

            this._apiDbContext.Parts.Add(part);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Models.Part part)
        {
            var mine = MyParts.FirstOrDefault(p => p.id == id);
            if (mine == null)
            {
                return NotFound();
            }

            if (!OwnsCategory(part.categoryId))
            {
                return BadRequest();
            }

            mine.name = part.name;
            mine.categoryId = part.categoryId;
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var part = MyParts.FirstOrDefault(p => p.id == id);
            if (part == null)
            {
                return NotFound();
            }
            this._apiDbContext.Parts.Remove(part);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

    }
}
