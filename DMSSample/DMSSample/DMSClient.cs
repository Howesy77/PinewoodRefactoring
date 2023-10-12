using Pinewood.DMSSample.Business.Controllers;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business
{
    public class DMSClient
    {
        private readonly IPartInvoiceController _controller;

        public DMSClient(IPartInvoiceController controller)
        {
            _controller = controller;
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            return await _controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);
        }
    }
}