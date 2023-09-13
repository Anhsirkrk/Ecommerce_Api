namespace Ecommerce_Api.ViewModels
{
    public class LoginViewModel
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool UserFound { get; set; }
        public string ResultMessage { get; set; }

    }
}
