using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using VehicleApi.Interface;
using VehicleApi.Models;

namespace VehicleApi.Services;

public class VehicleService:IVehicle
{
    private AppDbContext _dbContext;
    public VehicleService()
    {
        _dbContext = new AppDbContext();
    }
    public async Task<List<Vehicle?>> GetAllVehicles()
    {
        var vehicle = await  _dbContext.Vehicles.ToListAsync();
        return vehicle;
    }

    public async Task<Vehicle?> GetVehicleById(int id)
    {
        return await _dbContext.Vehicles.FindAsync(id);
    }

    public async Task AddVehicle(Vehicle vehicle)
    {
        await _dbContext.Vehicles.AddAsync(vehicle);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateVehicle(int id, Vehicle vehicle)
    {
       var vehicleOb =  await _dbContext.Vehicles.FindAsync(id);
       if (vehicleOb != null)
       {
           vehicleOb.Name = vehicle.Name;
           vehicleOb.ImageUrl = vehicle.ImageUrl;
           vehicleOb.Height = vehicle.Height;
           vehicleOb.Width = vehicle.Width;
           vehicleOb.MaximumSpeed = vehicle.MaximumSpeed;
           vehicleOb.Price = vehicle.Price;
           vehicleOb.Displacement = vehicle.Displacement;
       }

       await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteVehicle(int id)
    {
        var vehicleOb = await _dbContext.Vehicles.FindAsync(id);
        _dbContext.Vehicles.Remove(vehicleOb);
        await _dbContext.SaveChangesAsync();
    }
}