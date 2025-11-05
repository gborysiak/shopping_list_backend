namespace dotnet_api.Models
{
    public class LoginVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseVM
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
