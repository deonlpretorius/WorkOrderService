using DigitalTwin.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using DigitalTwin.WorkOrderService.WebAPI.Services;
using DigitalTwin.WorkOrderService.WebAPI.Endpoints;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.AutomatedTests</c> contains the Web API automated test for the application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.AutomatedTests
{
    public class SiteEndpointsTests
    {
        private readonly Mock<ISiteService> _mockSiteService;

        public SiteEndpointsTests()
        {
            // Mock the backend service/repository layer.
            _mockSiteService = new Mock<ISiteService>();
        }

        [Fact]
        public async Task GetSiteById_ShouldReturnSite_WhenIdIsValid()
        {
            // Arrange the Mock data.
            var expectedSite = new Site()
            {
                SiteName = "This is a test site.",
                SiteDescription = "This is the description of the test site.",
                SiteCode = "TestSite"
            };
            _mockSiteService.Setup(s => s.GetByIdAsync("")) // Acquire the correct record ID from the database.
                            .ReturnsAsync(expectedSite);

            // Act - Call the Minumal API handler function directly in isolation
            var result = await SiteEndpoint.GetAllSites(_mockSiteService.Object);

            // Assert.
            // Verify that the underlying result variant is "OK".
            var okResult = Assert.IsType<Ok<Site>>(result);

            // Verify the status code and payload value are exactly as ecpected.
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
            Assert.Equal("TestSite", okResult.Value.SiteCode);
        }

        [Fact]
        public async Task GetSiteById_ReturnsNotFound_WhenSiteDoesNotExist()
        {
            // Arrange.
            _mockSiteService.Setup(s => s.GetByIdAsync(""))
                            .ReturnsAsync((Site?)null);

            // Act.
            var result = await SiteEndpoint.GetSiteById("", _mockSiteService.Object);

            //Assert.
            // Verify that the underlying result variant is "NotFound"
            var notFoundResult = Assert.IsType<NotFound>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetSiteBySiteCode_ShouldReturnSite_WhenSiteCodeIsValid()
        {
            // Arrange.
            var expectedSite = new Site()
            {
                SiteName = "This is a test site",
                SiteDescription = "This is the description of the test site.",
                SiteCode = "TestSite"
            };
            _mockSiteService.Setup(s => s.GetBySiteCodeAsync("TestSite"))
                            .ReturnsAsync(expectedSite);

            // Act - Call the minimal API handler function directly in isolation
            var result = await SiteEndpoint.GetSiteBySiteCode("TestSite", _mockSiteService.Object);


            // Verify that the underlying result variant is "OK".
            var okResult = Assert.IsType<Ok<Site>>(result);

            // Verify the status code and payload value are exactly as ecpected.
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
            Assert.Equal("TestSite", okResult.Value.SiteCode);
        }

        [Fact]
        public async Task GetSiteBySiteCode_ReturnsNotFound_WhenSiteDoesNotExist()
        {
            // Arrange.
            _mockSiteService.Setup(s => s.GetBySiteCodeAsync("TestSite1"))
                            .ReturnsAsync((Site?)null);

            // Act.
            var result = await SiteEndpoint.GetSiteById("TestSite1", _mockSiteService.Object);

            //Assert.
            // Verify that the underlying result variant is "NotFound"
            var notFoundResult = Assert.IsType<NotFound>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}