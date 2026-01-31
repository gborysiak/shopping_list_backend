using dotnet_api.Data;
using dotnet_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        [HttpGet]
        public IActionResult Get()
        {
            var lists = this._apiDbContext.ShoppingLists.Include(list => list.ShoppingListItem).ToList();

            return Ok(lists);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var list = this._apiDbContext.ShoppingLists.Where(shoppinglist => shoppinglist.Id == id)
                .Include(list => list.ShoppingListItem)
                .ToList();
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

            //if( list.ShoppingListItem != null)
            //{
            //    foreach(ShoppingListItem item in list.ShoppingListItem)
            //    {
            //        // check if exists
            //        var tempItem = this._apiDbContext.ShoppingListItem.Find(item.Id);
            //        if( tempItem == null)
            //        {
            //            // create
            //            this._apiDbContext.Add(item);
            //        }
            //        else
            //        {

            //        }
            //    }
            //}

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateShoppingList([FromBody] Models.ShoppingList list)
        {

            this._apiDbContext.ShoppingLists.Update(list);
            this._apiDbContext.SaveChanges();

            //if( list.ShoppingListItem != null)
            //{
            //    foreach(ShoppingListItem item in list.ShoppingListItem)
            //    {
            //        // check if exists
            //        var tempItem = this._apiDbContext.ShoppingListItem.Find(item.Id);
            //        if( tempItem == null)
            //        {
            //            // create
            //            this._apiDbContext.Add(item);
            //        }
            //        else
            //        {

            //        }
            //    }
            //}

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingList(int id)
        {
            var list = this._apiDbContext.ShoppingLists.Where(shoppinglist => shoppinglist.Id == id)
                .Include(list => list.ShoppingListItem)
                .ToList();
            if (list == null)
            {
                return NotFound();
            }

            // remove lists
            foreach (Models.ShoppingList sl in list)
            {
                foreach (ShoppingListItem item in sl.ShoppingListItem)
                {
                    this._apiDbContext.ShoppingListItem.Remove(item);
                }

                this._apiDbContext.ShoppingLists.Remove(sl);
                this._apiDbContext.SaveChanges();
            }
            return Ok();
        }


        [HttpPut("{id}/Item/{itemId}")]
        public IActionResult UpdateItem(int id, int itemId, [FromBody] Models.ShoppingListItem item)
        {
            this._apiDbContext.ShoppingListItem.Update(item);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}/Item/{itemId}")]
        public IActionResult DeleteItem(int id, int itemId)
        {
            var item = this._apiDbContext.ShoppingListItem.Find(itemId);
            if (item == null)
            {
                return NotFound();
            }

            this._apiDbContext.ShoppingListItem.Remove(item);
            this._apiDbContext.SaveChanges();

            return Ok();
        }


        //[HttpPost("{id}/Item")]
        //public IActionResult CreateItem(int id,[FromBody] Models.ShoppingListItem item)
        //{

        //    this._apiDbContext.ShoppingListItem.Add(item);
        //    this._apiDbContext.SaveChanges();

        //    return Ok();
        //}

    }
}

