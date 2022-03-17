using VehicleApi.Models;

namespace VehicleApi.Interface;

public interface IVehicle
{
    Task<List<Vehicle?>> GetAllVehicles();
    Task<Vehicle?> GetVehicleById(int id);
    Task AddVehicle(Vehicle vehicle);
    Task UpdateVehicle(int id, Vehicle vehicle);
    Task DeleteVehicle(int id);
}