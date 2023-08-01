using Moq;
using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using DataAccessLayer.Repository;


namespace UnitTest_MedicineApp.Tests
{
    [TestFixture]
    public class DiseaseServiceTests
    {
        private DiseaseService _diseaseService;
        private Mock<IDiseaseRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IDiseaseRepository>();
            _diseaseService = new DiseaseService(_mockRepository.Object);
        }

        [Test]
        public void GetDiseaseCategories_Should_Return_List_Of_Categories()
        {
            // Arrange
            var expectedCategories = new List<string> { "Diabetes", "Cardiac", "Stomach", "Liver", "Kidney" };

            // Act
            var actualCategories = _diseaseService.GetDiseaseCategories();

            // Assert
            Assert.AreEqual(expectedCategories.Count, actualCategories.Count);
            for (int i = 0; i < expectedCategories.Count; i++)
            {
                Assert.AreEqual(expectedCategories[i], actualCategories[i]);
            }
        }

        [Test]
        public void AddDisease_Should_Call_Repository_Add_And_Save_Methods()
        {
            // Arrange
            var disease = new Disease { DiseaseCategory = "Test Disease" };

            // Act
            _diseaseService.AddDisease(disease);

            // Assert
            _mockRepository.Verify(r => r.Add(disease), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void AddDisease_Should_Throw_Exception_If_Repository_Throws_Exception()
        {
            // Arrange
            var disease = new Disease { DiseaseCategory = "Test Disease" };
            _mockRepository.Setup(r => r.Add(disease)).Throws(new Exception());

            // Act & Assert
            Assert.Throws<Exception>(() => _diseaseService.AddDisease(disease));
        }
    }
}