using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Models
{
    [Table("shoppinglistitem")]
    public class ShoppingListItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Part")]
        public int PartRefId { get; set; }
        //public Part part { get; set; }
        [Required]
        public float Quantity { get; set; }
        [Required]
        public bool Purchased { get; set; }

        public DateTime? PurchaseDate { get; set; }

        //public Part Part { get; }
        //public int ShoppingListId { get; set; }
        //public ShoppingList ShoppingList { get; set; }
    }
}
