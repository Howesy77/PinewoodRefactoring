using System.Data;
using System.Data.SqlClient;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Domain
{
    public class PartInvoiceRepositoryDb : IPartInvoiceRepositoryDb
    {
        private readonly IDbConnection _databaseConnection;

        public PartInvoiceRepositoryDb(IDbConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void Add(PartInvoice invoice)
        {
            using (var command = _databaseConnection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PMS_AddPartInvoice";

                var stockCodeParameter = new SqlParameter("@StockCode", SqlDbType.VarChar, 50) { Value = invoice.StockCode };
                command.Parameters.Add(stockCodeParameter); 
                
                var quantityParameter = new SqlParameter("@Quantity", SqlDbType.Int) { Value = invoice.Quantity };
                command.Parameters.Add(quantityParameter);
                
                var customerIdParameter = new SqlParameter("@CustomerID", SqlDbType.Int) { Value = invoice.CustomerId };
                command.Parameters.Add(customerIdParameter);

                _databaseConnection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
