namespace Entra21_TCC_BackEnd_UpCommerce.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ?urlPhoto { get; set; }
        public string ?urlLinkedin { get; set; }
        public string ?urlInstagram { get; set; }
    }
}
