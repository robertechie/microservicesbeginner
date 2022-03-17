using Microsoft.AspNetCore.Mvc;
using VehicleApi.Interface;
using VehicleApi.Models;

namespace VehicleApi.Controllers;
[Route("Api/[controller]")]
[ApiController]
public class VehicleController : ControllerBase
{
    private readonly IVehicle _vehicles;

    public VehicleController(IVehicle vehicles)
    {
        _vehicles = vehicles;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        var vehicle = await _vehicles.GetAllVehicles();
        return Ok(vehicle);
    }
    //
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllVehicleById(int id)
    {
        var vehicle = await _vehicles.GetVehicleById(id);
        return Ok(vehicle);
    }
    //
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Vehicle vehi)
    {
        await _vehicles.AddVehicle(vehi);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody] Vehicle vehi)
    {
        await _vehicles.UpdateVehicle(id, vehi);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _vehicles.DeleteVehicle(id);
        return NoContent();
    }
}