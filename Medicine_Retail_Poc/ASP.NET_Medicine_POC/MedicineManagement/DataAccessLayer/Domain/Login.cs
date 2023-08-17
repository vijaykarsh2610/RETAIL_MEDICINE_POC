using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.Domain
{

    public class Login
    {
        [Key] // This property is the primary key
        public int Id { get; set; }

        // add name property

        [Required(ErrorMessage = "Name is required.")]
        public string Name = "";

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "IsAdmin is required.")]
        public bool IsAdmin { get; set; }
    }

}