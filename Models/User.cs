using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace dotnet_api.Models
{
    public class User : IdentityUser<int>
    {
        //public string username { get; set; } 
        //public string password { get; set; }
        public string name { get; set; }

        
        //public string email { get; set; }
        public string? token { get; set; }


        //public Role[] roles {  get; set; }

        public DateTime createdAt { get; set; }

        public DateTime? lastLoggedIn { get; set; }

        //  avatar?: Uint8Array;
        //avatarBase64?: string;
    }
}
