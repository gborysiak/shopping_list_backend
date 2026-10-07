using dotnet_api.Data;
using dotnet_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // listes de l'utilisateur connecté uniquement
        private IQueryable<Models.ShoppingList> MyLists =>
            this._apiDbContext.ShoppingLists.Where(l => l.OwnerId == UserId);

        [HttpGet]
        public IActionResult Get()
        {
            var lists = MyLists.Include(list => list.ShoppingListItem).ToList();

            return Ok(lists);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var list = MyLists.Where(shoppinglist => shoppinglist.Id == id)
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
            list.OwnerId = UserId;
            this._apiDbContext.ShoppingLists.Add(list);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("/api/ResetShoppingList")]
        public IActionResult ResetShoppingList([FromBody] Models.ShoppingList list)
        {
            if (list == null)
            {
                return NotFound();
            }

            var mine = MyLists.Include(l => l.ShoppingListItem).FirstOrDefault(l => l.Id == list.Id);
            if (mine == null)
            {
                return NotFound();
            }

            foreach (ShoppingListItem item in mine.ShoppingListItem)
            {
                item.Purchased = false;
                item.PurchaseDate = null;
            }

            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateShoppingList([FromBody] Models.ShoppingList list)
        {
            if (!MyLists.Any(l => l.Id == list.Id))
            {
                return NotFound();
            }

            // un item existant doit déjà appartenir à cette liste
            var itemIds = list.ShoppingListItem?.Where(i => i.Id != 0).Select(i => i.Id).ToList() ?? [];
            var owned = this._apiDbContext.ShoppingListItem
                .Count(i => itemIds.Contains(i.Id) && EF.Property<int?>(i, "ShoppingListId") == list.Id);
            if (owned != itemIds.Count)
            {
                return BadRequest();
            }

            list.OwnerId = UserId;
            this._apiDbContext.ShoppingLists.Update(list);
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingList(int id)
        {
            var list = MyLists.Where(shoppinglist => shoppinglist.Id == id)
                .Include(list => list.ShoppingListItem)
                .ToList();
            if (list.Count == 0)
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

        // item de la liste {id}, à condition que la liste soit à l'utilisateur
        private ShoppingListItem? MyItem(int id, int itemId)
        {
            var uid = UserId;
            return this._apiDbContext.ShoppingListItem.FirstOrDefault(i => i.Id == itemId
                && EF.Property<int?>(i, "ShoppingListId") == id
                && this._apiDbContext.ShoppingLists.Any(l => l.Id == id && l.OwnerId == uid));
        }

        [HttpPut("{id}/Item/{itemId}")]
        public IActionResult UpdateItem(int id, int itemId, [FromBody] Models.ShoppingListItem item)
        {
            var mine = MyItem(id, itemId);
            if (mine == null)
            {
                return NotFound();
            }

            mine.PartRefId = item.PartRefId;
            mine.Quantity = item.Quantity;
            mine.Purchased = item.Purchased;
            mine.PurchaseDate = item.PurchaseDate;
            this._apiDbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}/Item/{itemId}")]
        public IActionResult DeleteItem(int id, int itemId)
        {
            var item = MyItem(id, itemId);
            if (item == null)
            {
                return NotFound();
            }

            this._apiDbContext.ShoppingListItem.Remove(item);
            this._apiDbContext.SaveChanges();

            return Ok();
        }
    }
}
