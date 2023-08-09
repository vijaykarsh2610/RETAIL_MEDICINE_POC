using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace UnitTest_MedicineApp
{
    [TestFixture]
    public class MedicineRepositoryTests
    {
        private ApplicationDbContext _context;
        private IMedicineRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new MedicineRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void Add_AddsMedicineToDatabase()
        {
            // Arrange
            var medicine = new Medicine
            {
                medicine_name = "Test Medicine",
                brand_name = "Test Brand",
                disease_category = "Test Category",
                cost = 10.0f,
                mfd = DateTime.Now,
                Expd = DateTime.Now.AddYears(1),
                weight = "100gm"
            };

            // Act
            _repository.Add(medicine);

            // Assert
            var result = _context.Medicines.FirstOrDefault(m => m.Id == medicine.Id);
            Assert.That(result, Is.EqualTo(medicine));
        }

        [Test]
        public void Update_UpdatesMedicineInDatabase()
        {
            // Arrange
            var medicine = new Medicine
            {
                medicine_name = "Test Medicine",
                brand_name = "Test Brand",
                disease_category = "Test Category",
                cost = 30.0f,
                mfd = DateTime.Now,
                Expd = DateTime.Now.AddYears(3),
                weight = "300gm"
            };
            _repository.Add(medicine);
            _context.SaveChanges();

            // Act
            medicine.cost = 20.0f;
            _repository.Update(medicine);

            // Assert
            var result = _context.Medicines.FirstOrDefault(m => m.Id == medicine.Id);
            Assert.That(result.cost, Is.EqualTo(20.0f));
        }

        [Test]
        public void Delete_DeletesMedicineFromDatabase()
        {
            // Arrange
            var medicine = new Medicine
            {
                medicine_name = "Test Medicine",
                brand_name = "Test Brand",
                disease_category = "Test Category",
                cost = 10.0f,
                mfd = DateTime.Now,
                Expd = DateTime.Now.AddYears(1),
                weight = "100gm"
            };
            _repository.Add(medicine);
            _context.SaveChanges();

            // Act
            _repository.Delete(medicine);

            // Assert
            var result = _context.Medicines.FirstOrDefault(m => m.Id == medicine.Id);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAllMedicines_ReturnsAllMedicinesFromDatabase()
        {
            // Arrange
            var medicines = new List<Medicine>
            {
                new Medicine
                {
                medicine_name = "Test Medicine1",
                brand_name = "Test Brand1",
                disease_category = "Test Category1",
                cost = 10.0f,
                mfd = DateTime.Now,
                Expd = DateTime.Now.AddYears(1),
                weight = "100gm"
                },
                new Medicine
                {
                medicine_name = "Test Medicine2",
                brand_name = "Test Brand2",
                disease_category = "Test Category2",
                cost = 20.0f,
                mfd = DateTime.Now,
                Expd = DateTime.Now.AddYears(2),
                 weight = "200gm"
                }
                // ... (add more medicine instances)
            };
        }
    }
            
 }
