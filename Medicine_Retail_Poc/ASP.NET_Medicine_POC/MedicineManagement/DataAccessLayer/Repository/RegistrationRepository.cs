using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repository
{
    public interface IRegistrationRepository
    {
        Task<bool> EmailExists(string email);
        Task<Registration> Create(Registration user);
        Task<Registration> Authenticate(string email, string password, bool isAdmin);
    }

    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<RegistrationRepository> _logger;
        public async Task<bool> EmailExists(string email)
        {
            return await _context.Registrations.AnyAsync(r => r.Email == email);
        }
        public RegistrationRepository(ApplicationDbContext context,ILogger<RegistrationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Registration> Create(Registration user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                // Hash the user's password before saving to the database
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Registrations.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException ex)
            {
                // Handle any other exceptions that may occur during user creation
                _logger.LogError($"Error creating user: {ex.Message}");
                throw new Exception("Failed to create user.", ex);
            }
        }

        // Authenticate a user by email and password
        public async Task<Registration> Authenticate(string email, string password, bool isAdmin)
        {
            try
            {
                var registration = await _context.Registrations.SingleOrDefaultAsync(u => u.Email == email);

                if (registration == null || !BCrypt.Net.BCrypt.Verify(password, registration.Password))
                {
                    return null;
                }

                // Check if the user is an admin based on the IsAdmin property in the registration
                if (isAdmin && !registration.IsAdmin)
                {
                    return null; // If isAdmin is true but user is not an admin, return null
                }

                return registration;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during user authentication
                _logger.LogError($"Error authenticating user: {ex.Message}");
                throw; // Re-throw the exception to be handled at the higher level
            }
        }
    }
}
