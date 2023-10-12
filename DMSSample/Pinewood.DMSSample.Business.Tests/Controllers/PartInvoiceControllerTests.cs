using Moq;
using Pinewood.DMSSample.Business.Clients;
using Pinewood.DMSSample.Business.Controllers;
using Pinewood.DMSSample.Business.Domain;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Tests.Controllers
{
    [TestFixture]
    public class PartInvoiceControllerTests
    {
        private Mock<ICustomerRepositoryDb> _customerRepositoryMock;
        private Mock<IPartInvoiceRepositoryDb> _partInvoiceRepositoryMock;
        private Mock<IPartAvailabilityClient> _partAvailabilityClientMock;
        private PartInvoiceController _controller;

        [SetUp]
        public void Setup()
        {
            _customerRepositoryMock = new Mock<ICustomerRepositoryDb>();
            _partInvoiceRepositoryMock = new Mock<IPartInvoiceRepositoryDb>();
            _partAvailabilityClientMock = new Mock<IPartAvailabilityClient>();

            _controller = new PartInvoiceController(
                _customerRepositoryMock.Object,
                _partInvoiceRepositoryMock.Object,
                _partAvailabilityClientMock.Object
            );
        }
        
        [Test]
        public async Task CreatePartInvoiceAsync_WithValidInput_ShouldReturnSuccessResult()
        {
            // Arrange
            var customer = new Customer(1, "Test McTest", "Test House");
            const string stockCode = "ABC123";
            const int availability = 100;
            const int quantity = 1;

            _customerRepositoryMock.Setup(repo => repo.GetByName(customer.Name)).Returns(customer);
            _partAvailabilityClientMock.Setup(client => client.GetAvailability(stockCode)).ReturnsAsync(availability);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customer.Name);

            // Assert
            Assert.That(result.Success, Is.True);
        }

        [Test]
        public async Task CreatePartInvoiceAsync_WithEmptyStockCode_ShouldReturnFailedResult()
        {
            // Arrange
            var customer = new Customer(1, "Test McTest", "Test House");
            const string stockCode = "";
            const int availability = 100;
            const int quantity = 1;

            _customerRepositoryMock.Setup(repo => repo.GetByName(customer.Name)).Returns(customer);
            _partAvailabilityClientMock.Setup(client => client.GetAvailability(stockCode)).ReturnsAsync(availability);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customer.Name);

            // Assert
            Assert.That(result.Success, Is.False);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        public async Task CreatePartInvoiceAsync_WithQuantityEqualToOrBelowZero_ShouldReturnFailedResult(int quantity, bool expected)
        {
            // Arrange
            var customer = new Customer(1, "Test McTest", "Test House");
            const string stockCode = "ABC123";
            const int availability = 100;

            _customerRepositoryMock.Setup(repo => repo.GetByName(customer.Name)).Returns(customer);
            _partAvailabilityClientMock.Setup(client => client.GetAvailability(stockCode)).ReturnsAsync(availability);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customer.Name);

            // Assert
            Assert.That(result.Success, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        public async Task CreatePartInvoiceAsync_WithInvalidCustomer_ShouldReturnFailedResult(int customerId, bool expected)
        {
            // Arrange
            var customer = new Customer(customerId, "Test McTest", "Test House");
            const string stockCode = "ABC123";
            const int availability = 100;
            const int quantity = 1;

            _customerRepositoryMock.Setup(repo => repo.GetByName(customer.Name)).Returns(customer);
            _partAvailabilityClientMock.Setup(client => client.GetAvailability(stockCode)).ReturnsAsync(availability);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customer.Name);

            // Assert
            Assert.That(result.Success, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        public async Task CreatePartInvoiceAsync_WithInsufficientQuantity_ShouldReturnFailedResult(int availability, bool expected)
        {
            // Arrange
            var customer = new Customer(1, "Test McTest", "Test House");
            const string stockCode = "ABC123";
            const int quantity = 1;

            _customerRepositoryMock.Setup(repo => repo.GetByName(customer.Name)).Returns(customer);
            _partAvailabilityClientMock.Setup(client => client.GetAvailability(stockCode)).ReturnsAsync(availability);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customer.Name);

            // Assert
            Assert.That(result.Success, Is.EqualTo(expected));
        }

        /*
         * Should the actual test for this be to validate there is enough availability for the quantity required?
        [Test]
        [TestCase(10, 1, true)]
        [TestCase(10,10, true)]
        [TestCase(10, 11, false)]
        [TestCase(0, 1, false)]
        public async Task CreatePartInvoiceAsync_WithInsufficientQuantity_ShouldReturnFailedResult(int availability, int quantity, bool expected)
        {
            // Arrange
            var customer = new Customer(1, "Test McTest", "Test House");
            const string stockCode = "ABC123";

            _customerRepositoryMock.Setup(repo => repo.GetByName(customer.Name)).Returns(customer);
            _partAvailabilityClientMock.Setup(client => client.GetAvailability(stockCode)).ReturnsAsync(availability);

            // Act
            var result = await _controller.CreatePartInvoiceAsync(stockCode, quantity, customer.Name);

            // Assert
            Assert.That(result.Success, Is.EqualTo(expected));
        }
         */
    }
}
