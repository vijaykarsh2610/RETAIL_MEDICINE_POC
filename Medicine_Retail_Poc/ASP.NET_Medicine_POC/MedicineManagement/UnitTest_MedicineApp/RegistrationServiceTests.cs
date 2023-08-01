using BCrypt.Net;
using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Moq;

namespace UnitTest_MedicineApp
{
    [TestFixture]
    public class RegistrationServiceTests
    {
        private Mock<IRegistrationRepository> _repositoryMock;
        private RegistrationService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRegistrationRepository>();
            _service = new RegistrationService(_repositoryMock.Object);
        }

        [Test]
        public async Task Create_ValidUser_ReturnsUser()
        {
            // Arrange
            var user = new Registration
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false
            };
            _repositoryMock.Setup(x => x.Create(user)).ReturnsAsync(user);

            // Act
            var result = await _service.Create(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            //Assert.IsTrue(BCrypt.Net.BCrypt.Verify(user.Password, result.Password));
        }
    }
}