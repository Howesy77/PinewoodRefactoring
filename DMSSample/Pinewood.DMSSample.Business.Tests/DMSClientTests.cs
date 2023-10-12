using Moq;
using Pinewood.DMSSample.Business.Controllers;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Tests
{
    public class Tests
    {
        private Mock<IPartInvoiceController> _partInvoiceControllerMock;
        private DMSClient _client;
        
        [SetUp]
        public void Setup()
        {
            _partInvoiceControllerMock = new Mock<IPartInvoiceController>();
            _client = new DMSClient(_partInvoiceControllerMock.Object);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task CreatePartInvoiceAsync_WithValidInput_ShouldReturnSuccessResult(bool success)
        {
            // Arrange
            var stockCode = "ABC123";
            var quantity = 100;
            var customerName = "Test";
            
            _partInvoiceControllerMock
                .Setup(ctrl => ctrl.CreatePartInvoiceAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new CreatePartInvoiceResult(success));

            // Act
            var result = await _client.CreatePartInvoiceAsync(stockCode, quantity, customerName);


            // Assert
            Assert.That(result.Success, Is.EqualTo(success));
        }
    }
}