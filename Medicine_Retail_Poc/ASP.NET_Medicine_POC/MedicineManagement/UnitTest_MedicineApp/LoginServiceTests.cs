using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Moq;


namespace UnitTest_MedicineApp
{
    [TestFixture]
    public class LoginServiceTests
    {
        private Mock<ILoginRepository> _repositoryMock;
        private LoginService _service;

        [SetUp]
        public void Initialize()
        {
            // Set up the mock repository and service for each test
            _repositoryMock = new Mock<ILoginRepository>();
            _service = new LoginService(_repositoryMock.Object);
        }

        [Test]
        public async Task Authenticate_ValidCredentials_ReturnsLogin()
        {
            // Arrange
            var model = new Login
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false
            };
            var login = new Login
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false
            };
            _repositoryMock.Setup(r => r.Authenticate(model.Email, model.Password, model.IsAdmin, model.Name))
                .ReturnsAsync(login);

            // Act
            var result = await _service.Authenticate(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(login.Email, result.Email);
            Assert.AreEqual(login.IsAdmin, result.IsAdmin);
            Assert.AreEqual(login.Password, result.Password);
        }

        [Test]
        public async Task Authenticate_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var model = new Login
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false,
                Name = "Ramesh"
            };
            _repositoryMock.Setup(r => r.Authenticate(model.Email, model.Password, model.IsAdmin, model.Name))
                .ReturnsAsync((Login)null);

            // Act
            var result = await _service.Authenticate(model);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Authenticate_ExceptionThrown_ReturnsNull()
        {
            // Arrange
            var model = new Login
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false,
                Name = "Ramesh"
            };
            _repositoryMock.Setup(r => r.Authenticate(model.Email, model.Password, model.IsAdmin, model.Name))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _service.Authenticate(model);

            // Assert
            Assert.IsNull(result);
        }
    }
}
