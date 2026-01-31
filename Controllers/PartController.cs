using dotnet_api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public IActionResult Get()
        {
            var parts = this._apiDbContext.Parts.ToList();

            return Ok(parts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var part = this._apiDbContext.Parts.Find(id);
            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        [HttpPost]
        public IActionResult CreatePart([FromBody] Models.Part part)
        {
            part.dateCreated = DateTime.Now;

            this._apiDbContext.Parts.Add(part);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Models.Part part)
        {
            //var oldpart = this._apiDbContext.Parts.Find(id);
            //if (oldpart == null)
            //{
            //    return NotFound();
            //}
            this._apiDbContext.Parts.Update(part);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var part = this._apiDbContext.Parts.Find(id);
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
