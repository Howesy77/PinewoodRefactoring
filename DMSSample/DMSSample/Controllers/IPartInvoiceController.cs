using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Controllers
{
    public interface IPartInvoiceController
    {
        Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName);
    }
}
