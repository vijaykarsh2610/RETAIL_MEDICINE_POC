using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

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

        public RegistrationService(IRegistrationRepository repository)
        {
            _repository = repository;
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
               
                throw; // Re-throw the exception to be handled at the higher level
            }
        }
    }
}
