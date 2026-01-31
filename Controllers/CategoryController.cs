using dotnet_api.Data;
using dotnet_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public IActionResult Get()
        {
            var categories = this._apiDbContext.Categories.ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = this._apiDbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreatePart([FromBody] Models.Category category)
        {

            this._apiDbContext.Categories.Add(category);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Models.Category category)
        {

            this._apiDbContext.Categories.Update(category);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = this._apiDbContext.Categories.Find(id);
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
