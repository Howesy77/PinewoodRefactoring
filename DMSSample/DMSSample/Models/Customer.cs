namespace Pinewood.DMSSample.Business.Models
{
    public class Customer
    {
        public Customer(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; }
        public string Name { get; }
        public string Address { get; }
    }
}
