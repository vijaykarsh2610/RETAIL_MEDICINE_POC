using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace UnitTest_MedicineApp.Tests
{
    [TestFixture]
    public class DiseaseRepositoryTests
    {
        private ApplicationDbContext _context;
        private IDiseaseRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new DiseaseRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void Add_AddsDiseaseToDatabase()
        {
            // Arrange
            var disease = new Disease { DiseaseCategory = "Test Disease" };

            // Act
            _repository.Add(disease);

            // Assert
            Assert.AreEqual(1, _context.Diseases.Count());
            Assert.AreEqual(disease, _context.Diseases.Single());
        }

        [Test]
        public void GetAllDiseases_ReturnsAllDiseases()
        {
            // Arrange
            var disease1 = new Disease { DiseaseCategory = "Test Disease 1" };
            var disease2 = new Disease { DiseaseCategory = "Test Disease 2" };
            _context.Diseases.AddRange(disease1, disease2);
            _context.SaveChanges();

            // Act
            var diseases = _repository.GetAllDiseases();

            // Assert
            Assert.AreEqual(2, diseases.Count());
            Assert.Contains(disease1, (System.Collections.ICollection?)diseases);
            Assert.Contains(disease2, (System.Collections.ICollection?)diseases);
        }

        [Test]
        public void Save_SavesChangesToDatabase()
        {
            // Arrange
            var disease = new Disease { DiseaseCategory = "Test Disease" };
            _context.Diseases.Add(disease);

            // Act
            _repository.Save();

            // Assert
            Assert.AreEqual(1, _context.Diseases.Count());
            Assert.AreEqual(disease, _context.Diseases.Single());
        }
    }
}
