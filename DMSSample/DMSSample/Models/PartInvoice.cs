namespace Pinewood.DMSSample.Business.Models
{
    public class PartInvoice
    {
        public PartInvoice(string stockCode, int quantity, int customerId)
            : this(-1, stockCode, quantity, customerId)
        {
        }

        public PartInvoice(int id, string stockCode, int quantity, int customerId)
        {
            Id = id;
            StockCode = stockCode;
            Quantity = quantity;
            CustomerId = customerId;
        }

        public int Id { get; set; }
        public string StockCode { get; }
        public int Quantity { get; }
        public int CustomerId { get; }
    }
}
