using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataAccessLayer.Repository
{
    public interface ILoginRepository
    {
        Task<Login> Authenticate(string email, string password, bool isAdmin,string N);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Login> Authenticate(string email, string password, bool isAdmin, string N)
        {
            try
            {
                // Find the user with the given email
                var user = await _context.Registrations.FirstOrDefaultAsync(u => u.Email == email);

                Debug.WriteLine(user.FirstName);

                // If user is not found or password does not match, return null
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return null;
                }

                // Check if the user is an admin based on the IsAdmin property in the registration
                if (isAdmin && !user.IsAdmin)
                {
                    return null; // If isAdmin is true but user is not an admin, return null
                }


                // Create a new Login object with the user's information
                var login = new Login();
                login.Email = email;
                login.Password = password;
                login.IsAdmin = user.IsAdmin;
                login.Name = user.FirstName ;

                return login;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the authentication process
                // You can log the error or perform any other actions here
                Console.WriteLine("Error occurred during authentication: " + ex.Message);
                return null;
            }
        }
    }
}
