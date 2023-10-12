using System.Data;
using Moq;
using Pinewood.DMSSample.Business.Domain;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Tests.Domain
{
    [TestFixture]
    public class CustomerRepositoryDbTests
    {
        private Mock<IDbConnection> _connectionMock;
        private Mock<IDbCommand> _commandMock;
        private Mock<IDataReader> _dataReaderMock;
        private CustomerRepositoryDb _customerRepositoryDb;

        [SetUp]
        public void Setup()
        {
            _connectionMock = new Mock<IDbConnection>();
            _commandMock = new Mock<IDbCommand>();
            _customerRepositoryDb = new CustomerRepositoryDb(_connectionMock.Object);
            _dataReaderMock = new Mock<IDataReader>();
        }

        [Test]
        public void GetByName_WithValidName_ReturnsCustomer()
        {
            // Arrange
            var expectedCustomer = new Customer(1, "Testy Test", "My Home");

            _dataReaderMock.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(false);

            _dataReaderMock.Setup(r => r["CustomerID"]).Returns(expectedCustomer.Id);
            _dataReaderMock.Setup(r => r["Name"]).Returns(expectedCustomer.Name);
            _dataReaderMock.Setup(r => r["Address"]).Returns(expectedCustomer.Address);
            
            _commandMock.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(_dataReaderMock.Object);
            _commandMock.Setup(m => m.Parameters.Add(It.IsAny<IDbDataParameter>())).Verifiable();
            _connectionMock.Setup(c => c.CreateCommand()).Returns(_commandMock.Object);
            
            // Act
            var actualCustomer = _customerRepositoryDb.GetByName("John Doe");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualCustomer?.Name, Is.EqualTo(expectedCustomer.Name));
                Assert.That(actualCustomer?.Id, Is.EqualTo(expectedCustomer.Id));
                Assert.That(actualCustomer?.Address, Is.EqualTo(expectedCustomer.Address));
            });
        }
    }
}
