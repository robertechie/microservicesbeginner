using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomersApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomer _customerservice;

    public CustomerController(ICustomer customerservice)
    {
        _customerservice = customerservice;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Customer customer)
    {
        await _customerservice.AddCustomer(customer);
        return NoContent();
    }
}