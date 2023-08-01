using DataAccessLayer.Domain;
using DataAccessLayer.Repository;

namespace BusinessLogicLayer.Services
{
    public interface ILoginService
    {
        Task<Login> Authenticate(Login model);
    }

    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;

        public LoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<Login> Authenticate(Login model)
        {
            try
            {
                // Call the Authenticate method of the repository to validate the login credentials
                var login = await _repository.Authenticate(model.Email, model.Password, model.IsAdmin, model.Name);

                // If the login credentials are not valid, return null
                if (login == null)
                {
                    return null;
                }

                // Set IsAdmin based on the user role from the repository
                model.IsAdmin = login.IsAdmin;
                //model.Name = "Ramesh";
                model.Name = login.Name ;

                return login;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the authentication process
               
                return null;
            }
        }
    }
}
