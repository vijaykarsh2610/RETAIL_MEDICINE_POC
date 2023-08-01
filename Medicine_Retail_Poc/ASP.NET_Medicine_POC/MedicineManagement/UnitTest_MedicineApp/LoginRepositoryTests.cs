using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;


namespace UnitTest_MedicineApp
{
    [TestFixture]
    public class LoginRepositoryTests
    {
        private ApplicationDbContext _context;
        private LoginRepository _repository;

        [SetUp]
        public void Initialize()
        {
            // Set up the database context and repository for each test
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new LoginRepository(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            // Clean up the database context after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Authenticate_ValidUser_ReturnsLogin()
        {
            // Arrange
            var user = new Registration
            {
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                IsAdmin = false,
                FirstName = "John",
                LastName = "Doe",
                Contact = "1234567890",
                ConfirmPassword = "password"
            };
            _context.Registrations.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Authenticate(user.Email, "password", false, user.FirstName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            Assert.AreEqual("password", result.Password);
        }

        [Test]
        public async Task Authenticate_InvalidEmail_ReturnsNull()
        {
            // Arrange
            var user = new Registration
            {
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                IsAdmin = false,
                FirstName = "John",
                LastName = "Doe",
                Contact = "1234567890",
                ConfirmPassword = "password"
            };
            _context.Registrations.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Authenticate("invalid@example.com", "password", false, user.FirstName);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Authenticate_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var user = new Registration
            {
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                IsAdmin = false,
                FirstName = "John",
                LastName = "Doe",
                Contact = "1234567890",
                ConfirmPassword = "password"
            };
            _context.Registrations.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Authenticate(user.Email, "invalid", false, user.FirstName);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Authenticate_AdminUser_ReturnsLogin()
        {
            // Arrange
            var user = new Registration
            {
                Email = "admin@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                IsAdmin = true,
                FirstName = "Admin",
                LastName = "User",
                Contact = "1234567890",
                ConfirmPassword = "password"
            };
            _context.Registrations.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Authenticate(user.Email, "password", true, user.FirstName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            Assert.AreEqual("password", result.Password);
        }

        [Test]
        public async Task Authenticate_NonAdminUser_ReturnsNull()
        {
            // Arrange
            var user = new Registration
            {
                Email = "test@example.com",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                IsAdmin = false,
                FirstName = "John",
                LastName = "Doe",
                Contact = "1234567890",
                ConfirmPassword = "password"
            };
            _context.Registrations.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Authenticate(user.Email, "password", true, user.FirstName);

            // Assert
            Assert.IsNull(result);
        }
    }
}
