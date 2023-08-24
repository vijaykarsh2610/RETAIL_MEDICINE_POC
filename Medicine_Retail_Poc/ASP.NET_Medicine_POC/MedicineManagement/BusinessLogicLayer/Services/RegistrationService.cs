using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer.Services
{
    public interface IRegistrationService
    {
        Task<bool> EmailExists(string email);
        Task<Registration> Create(Registration user);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _repository;
        private readonly ILogger<RegistrationService> _logger;
        public async Task<bool> EmailExists(string email)
        {
            try
            {
                return await _repository.EmailExists(email);
            } 
            catch(Exception ex) { 

                throw;
            }

        }

        public RegistrationService(IRegistrationRepository repository, ILogger<RegistrationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Create a new user (registration) using the repository
        public async Task<Registration> Create(Registration model)
        {
            try
            {
                // Call the Create method of the registration repository to save the user to the database
                return await _repository.Create(model);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during user creation
               _logger.LogError("Error occurred during user creation: " + ex.Message);
                throw; // Re-throw the exception to be handled at the higher level
            }
        }
    }
}
