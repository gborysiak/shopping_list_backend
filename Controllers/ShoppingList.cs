using dotnet_api.Data;
using dotnet_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace dotnet_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ShoppingList : ControllerBase
    {
        public readonly ApiDbContext _apiDbContext;
        public ShoppingList(ApiDbContext apiDbContext)
        {
            this._apiDbContext = apiDbContext;
        }

        /*
        [HttpGet]
        public IActionResult Get()
        {
            var parts = this._apiDbContext.Parts.ToList();

            return Ok(parts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var article = this._apiDbContext.Parts.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }
        */

        [HttpGet]
        public IActionResult Get()
        {
            var lists = this._apiDbContext.ShoppingLists.ToList();

            return Ok(lists);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var list = this._apiDbContext.ShoppingLists.Find(id);
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateShoppingList([FromBody] Models.ShoppingList list)
        {

            this._apiDbContext.ShoppingLists.Add(list);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        /*
        [HttpPost]
        public IActionResult CreateArticle([FromBody] Part article)
        {

            this._apiDbContext.Parts.Add(article);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult EditArticle([FromBody] Part article)
        {
            this._apiDbContext.Parts.Update(article);
            this._apiDbContext.SaveChanges();

            return Ok();
        }
        */
    }
}

