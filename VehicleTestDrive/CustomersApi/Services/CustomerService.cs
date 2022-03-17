using Azure.Messaging.ServiceBus;
using CustomersApi.Data;
using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomersApi.Services;

public class CustomerService:ICustomer
{
    private AppDbContext _dbContext;
    public CustomerService()
    {
        _dbContext = new AppDbContext();
    }
    public async Task AddCustomer(Customer customer)
    {
        var vehicleInDb = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == customer.VehicleId);
        if (vehicleInDb == null)
        {
            await _dbContext.Vehicles.AddAsync(customer.Vehicle);
            await _dbContext.SaveChangesAsync();
        }

        customer.Vehicle = null;
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();

        //sending a message to the service bus
        string connectionString = "Endpoint=sb://vehimicro.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8ADk0KDZe4TKf8gLPJpyFAp85cEoVum8FeqmMTbAzBU=";
        string queueName = "vehimicro";
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
        await using var client = new ServiceBusClient(connectionString);

        //serialize the object
        var objAsText = JsonConvert.SerializeObject(customer);

// create the sender
        ServiceBusSender sender = client.CreateSender(queueName);

// create a message that we can send. UTF-8 encoding is used when providing a string.
        ServiceBusMessage message = new ServiceBusMessage(objAsText);

// send the message
        await sender.SendMessageAsync(message);
    }
}