using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Domain
{
    public class Registration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your First Name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "First Name must contain only letters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters long.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Last Name must contain only letters.")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]

        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your Contact No.")]
        [MinLength(10, ErrorMessage = "Contact No must be at least 10 characters long.")]

        public string? Contact { get; set; }

        [Required(ErrorMessage = "Please enter your Password.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }


        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }
    }



}

