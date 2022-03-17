using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationsApi.Data;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;

namespace ReservationsApi.Services;

public class ReservationService:IReservation
{
    private AppDbContext _dbContext;

    public ReservationService()
    {
        _dbContext = new AppDbContext();
    }

    public async Task<List<Reservation>> GetReservation()
    {
        //sending a message to the service bus
        const string connectionString = "Endpoint=sb://vehimicro.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8ADk0KDZe4TKf8gLPJpyFAp85cEoVum8FeqmMTbAzBU=";
        const string queueName = "vehimicro";
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
        await using var client = new ServiceBusClient(connectionString);
        // create a receiver that we can use to receive the message
        ServiceBusReceiver receiver = client.CreateReceiver(queueName);
// the received message is a different type as it contains some service set properties
        IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);
        if (receivedMessages is null) return null!;
        foreach (var receivedMessage in receivedMessages)
        {
            var body = receivedMessage.Body.ToString();
            var objs = JsonConvert.DeserializeObject<Reservation>(body);
            await _dbContext.Reservations.AddAsync(objs!);
            await _dbContext.SaveChangesAsync();
            await receiver.CompleteMessageAsync(receivedMessage);
        }
        //close the service bus code here
        return await _dbContext.Reservations.ToListAsync();

    }

    public async Task UpdateMailStatus(int id)
    {
        //find the reservation from the database
        var theReservation = await _dbContext.Reservations.FindAsync(id);
        if (theReservation!=null && theReservation.IsEmailSent == false)
        {
            var smtp = new SmtpClient("smtp.live.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vehicletestdrive11@outlook.com", "Ahfh@113"),
                EnableSsl = true

            };
            smtp.Send("vehicletestdrive11@outlook.com",theReservation.Email,"Vehicle Test Drive","Your test drive is reserved");
            theReservation.IsEmailSent = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}