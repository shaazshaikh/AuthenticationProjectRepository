namespace AuthenticationProject.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
