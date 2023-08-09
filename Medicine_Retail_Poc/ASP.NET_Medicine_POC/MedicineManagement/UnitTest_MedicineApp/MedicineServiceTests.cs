using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Moq;


namespace UnitTest_MedicineApp
{
    [TestFixture]
    public class MedicineServiceTests
    {
        private Mock<IMedicineRepository> _repositoryMock;
        private MedicineService _service;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IMedicineRepository>();
            _service = new MedicineService(_repositoryMock.Object);
        }

        [Test]
        public void AddMedicine_CallsRepositoryAddAndSave()
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
            _service.AddMedicine(medicine);

            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<Medicine>()), Times.Once);
            _repositoryMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void AddMedicine_ThrowsException_WhenRepositoryAddFails()
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
            _repositoryMock.Setup(r => r.Add(It.IsAny<Medicine>())).Throws(new Exception("Add failed"));

            // Act & Assert
            Assert.Throws<Exception>(() => _service.AddMedicine(medicine));
        }

        [Test]
        public void GetDiseaseCategories_ReturnsListOfCategories()
        {
            // Arrange
            var expectedCategories = new List<string> { "Category1", "Category2", "Category3" };
            _repositoryMock.Setup(r => r.GetDiseaseCategories()).Returns(expectedCategories);
            var service = new MedicineService(_repositoryMock.Object);

            // Act
            var result = service.GetDiseaseCategories();

            // Assert
            Assert.AreEqual(expectedCategories, result);
        }
    }
}
