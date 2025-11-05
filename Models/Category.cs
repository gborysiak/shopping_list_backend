namespace dotnet_api.Models
{
    public class Category
    {
        public int id {  get; set; }

        public string name { get; set; }

        public Part[]  parts { get; set; }
    }
}
