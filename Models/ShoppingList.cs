using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Models
{
    [Table("shoppinglists")]
    public class ShoppingList
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public string Name { get; set; }

        //public ShoppingListItem[] ShoppingListItem { get; set; }
        public ICollection<ShoppingListItem> ShoppingListItem { get; set; }

        //public User[]? owners { get; set; }

        //public User[]?  sharedWith { get; set; }

        //shoppingListActions?: Action[]; /
    }
}
