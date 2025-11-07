using System.ComponentModel.DataAnnotations;

namespace dotnet_api.Models
{
    public class Part   
    {
        public int id { get; set; }
        public string name { get; set; }
        public float quantity { get; set; }

        public bool purchased { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime? purchaseDate {  get; set; }

        public string purchasedBy {  get; set; }

    }
}
