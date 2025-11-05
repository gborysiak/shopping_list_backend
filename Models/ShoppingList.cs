namespace dotnet_api.Models
{
    public class ShoppingList
    {
        public int id {  get; set; }

        public string name { get; set; }

        public Part[] parts { get; set; }

        public User[] owners { get; set; }
        
        public User[]  sharedWith { get; set; }

        //shoppingListActions?: Action[]; /
    }
}
