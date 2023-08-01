using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace UnitTest_MedicineApp
{

    [TestFixture]
    public class RegistrationRepositoryTests
    {
        private ApplicationDbContext _context;
        private RegistrationRepository _repository;

        [SetUp]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new RegistrationRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Registrations.RemoveRange(_context.Registrations);
            _context.SaveChanges();
        }

        [Test]
        public async Task Create_ValidUser_ReturnsUser()
        {
            // Arrange
            var user = new Registration
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false,
                FirstName = "John",
                LastName = "Doe",
                Contact = "1234567890",
                ConfirmPassword = "password"
            };

            // Act
            // Act
            var result = await _repository.Create(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Contact, result.Contact);
            Assert.AreEqual(user.ConfirmPassword, result.ConfirmPassword);
            Assert.AreEqual(user.Password, result.Password);
            Assert.AreEqual(1, _context.Registrations.Count());
        }

        [Test]
        public async Task Create_DuplicateEmail_ThrowsException()
        {
            // Arrange
            var user1 = new Registration
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false,
                ConfirmPassword = "password",
                Contact = "1234567890",
                FirstName = "John",
                LastName = "Doe"
            };
            var user2 = new Registration
            {
                Email = "test@example.com",
                Password = "password",
                IsAdmin = false,
                ConfirmPassword = "password",
                Contact = "1234567890",
                FirstName = "John",
                LastName = "Doe"
            };
            _context.Registrations.Add(user1);
            await _context.SaveChangesAsync();

            // Act & Assert
            Assert.ThrowsAsync<DbUpdateException>(() => _repository.Create(user2));

        }

        [Test]
        public async Task Create_NullUser_ThrowsException()
        {
            // Arrange
            Registration user = null;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Create(user));
        }
    }
}
