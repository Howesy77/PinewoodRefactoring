using Pinewood.DMSSample.Business.Clients;
using Pinewood.DMSSample.Business.Domain;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Controllers
{
    public class PartInvoiceController : IPartInvoiceController
    {
        private readonly ICustomerRepositoryDb _customerRepositoryDb;
        private readonly IPartInvoiceRepositoryDb _partInvoiceRepositoryDb;
        private readonly IPartAvailabilityClient _partAvailabilityClient;

        public PartInvoiceController(
            ICustomerRepositoryDb customerRepositoryDb,
            IPartInvoiceRepositoryDb partInvoiceRepositoryDb,
            IPartAvailabilityClient partAvailabilityClient
        )
        {
            _customerRepositoryDb = customerRepositoryDb;
            _partInvoiceRepositoryDb = partInvoiceRepositoryDb;
            _partAvailabilityClient = partAvailabilityClient;
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            if (string.IsNullOrEmpty(stockCode))
            {
                return new CreatePartInvoiceResult(false);
            }

            if (quantity <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            var customer = _customerRepositoryDb.GetByName(customerName);

            var customerId = customer?.Id ?? 0;
            if (customerId <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            var availability = await _partAvailabilityClient.GetAvailability(stockCode);
            if (availability <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            var partInvoice = new PartInvoice(
                stockCode: stockCode,
                quantity: quantity,
                customerId: customerId
            );

            _partInvoiceRepositoryDb.Add(partInvoice);

            return new CreatePartInvoiceResult(true);
        }
    }
}
