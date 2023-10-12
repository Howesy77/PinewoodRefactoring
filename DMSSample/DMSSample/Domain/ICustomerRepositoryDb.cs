using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Domain
{
    public interface ICustomerRepositoryDb
    {
        Customer? GetByName(string name);
    }
}
