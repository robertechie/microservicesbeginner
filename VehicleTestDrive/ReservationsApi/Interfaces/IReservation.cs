using ReservationsApi.Models;

namespace ReservationsApi.Interfaces;

public interface IReservation
{
   public  Task<List<Reservation>> GetReservation();
   public Task UpdateMailStatus(int id);

}