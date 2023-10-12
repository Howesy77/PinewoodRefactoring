using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Domain
{
    public interface IPartInvoiceRepositoryDb
    {
        void Add(PartInvoice invoice);
    }
}
