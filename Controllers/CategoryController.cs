using dotnet_api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class CategoryController : Controller
    {
        public readonly ApiDbContext _apiDbContext;
        public CategoryController(ApiDbContext apiDbContext)
        {
            this._apiDbContext = apiDbContext;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // catégories de l'utilisateur connecté uniquement
        private IQueryable<Models.Category> MyCategories =>
            this._apiDbContext.Categories.Where(c => c.OwnerId == UserId);

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(MyCategories.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = MyCategories.FirstOrDefault(c => c.id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreatePart([FromBody] Models.Category category)
        {
            category.id = 0;
            category.OwnerId = UserId;

            this._apiDbContext.Categories.Add(category);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Models.Category category)
        {
            var mine = MyCategories.FirstOrDefault(c => c.id == category.id);
            if (mine == null)
            {
                return NotFound();
            }

            mine.name = category.name;
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = MyCategories.FirstOrDefault(c => c.id == id);
            if (category == null)
            {
                return NotFound();
            }
            this._apiDbContext.Categories.Remove(category);
            this._apiDbContext.SaveChanges();

            return Ok();
        }
    }
}
