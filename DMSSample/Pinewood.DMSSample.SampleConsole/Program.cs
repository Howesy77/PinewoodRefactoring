// See https://aka.ms/new-console-template for more information

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pinewood.DMSSample.Business;
using Pinewood.DMSSample.Business.Clients;
using Pinewood.DMSSample.Business.Controllers;
using Pinewood.DMSSample.Business.Domain;


var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"]?.ConnectionString ?? "";

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, services) =>
    {
        services.AddTransient<IDbConnection>(svc => new SqlConnection(connectionString));
        services.AddTransient<IPartInvoiceController, PartInvoiceController>();
        services.AddTransient<IPartAvailabilityClient, PartAvailabilityClient>();
        services.AddTransient<ICustomerRepositoryDb, CustomerRepositoryDb>();
        services.AddTransient<IPartInvoiceRepositoryDb, PartInvoiceRepositoryDb>();
    })
    .Build();

using (var serviceScope = host.Services.CreateScope())
{
    var provider = serviceScope.ServiceProvider;
    var partInvoiceController = provider.GetRequiredService<IPartInvoiceController>();

    var dmsClient = new DMSClient(partInvoiceController);
    await dmsClient.CreatePartInvoiceAsync("1234", 10, "John Doe");
}
