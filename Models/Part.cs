using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Models
{
    public class Part   
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }

        public int categoryId { get; set; }
        //public Category category { get; set; }

        public DateTime dateCreated { get; set; }


    }
}
