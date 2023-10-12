using System.Data;
using System.Data.SqlClient;
using Pinewood.DMSSample.Business.Models;

namespace Pinewood.DMSSample.Business.Domain
{
    public class CustomerRepositoryDb : ICustomerRepositoryDb
    {
        private readonly IDbConnection _databaseConnection;

        public CustomerRepositoryDb(IDbConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public Customer? GetByName(string name)
        {
            Customer? customer = null;

            using (var command = _databaseConnection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "CRM_GetCustomerByName";

                var parameter = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
                command.Parameters.Add(parameter);

                _databaseConnection.Open();

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    customer = new Customer(
                        id: (int)reader["CustomerID"],
                        name: (string)reader["Name"],
                        address: (string)reader["Address"]
                    );
                }
            }

            return customer;
        }
    }
}
