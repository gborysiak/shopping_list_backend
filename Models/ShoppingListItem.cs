using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Models
{
    public class ShoppingListItem
    {
        public int id { get; set; }

        [ForeignKey("Part")]
        public int PartRefId { get; set; }
        public Part part { get; set; }

        public float quantity { get; set; }

        public bool purchased { get; set; }

        public DateTime? purchaseDate { get; set; }
    }
}
