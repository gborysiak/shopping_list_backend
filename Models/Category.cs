using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_api.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }

        public string name { get; set; }

        // positionné côté serveur depuis le JWT, jamais lu du client
        public int OwnerId { get; set; }


    }
}
