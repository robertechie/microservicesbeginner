using Microsoft.AspNetCore.Mvc;
using ReservationsApi.Interfaces;
using ReservationsApi.Models;

namespace ReservationsApi.Controllers;
[Route("Api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IReservation _reservationService;

    public ReservationController(IReservation reservationService)
    {
        _reservationService = reservationService;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var reservations =await _reservationService.GetReservation();
        return Ok(reservations);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id)
    {
       await _reservationService.UpdateMailStatus(id);
       return NoContent();
    }
}