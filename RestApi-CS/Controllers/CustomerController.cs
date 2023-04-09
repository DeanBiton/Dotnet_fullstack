using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json;

using RestAPI.Services;
using Newtonsoft.Json.Linq;

namespace RestAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController()
    {
        _customerService = new CustomerService();
    }

    [HttpGet("")]
    async public Task<IActionResult> Get()
    {
        DataTable dt = await _customerService.Get();
        if (dt.Rows.Count > 0)
            return Ok(JsonConvert.SerializeObject(dt));
        else
            return NotFound();
    }

    [HttpPost("")]
    async public Task<IActionResult> Post([FromBody] JsonElement value)
    {
        int querySuccess = await _customerService.Post(value);
        if (querySuccess == 1)
            return Ok("Record inserted with the value as " + value);
        else
            return Ok("Try again. No data inserted");
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> Put(int id, [FromBody] JsonElement value)
    {
        int querySuccess = await _customerService.Put(id, value);
        if (querySuccess == 1)
            return Ok("Record updated with the value as " + value + "\nand id as " + id);
        else
            return Ok("Try again. No data updated");
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        int querySuccess = await _customerService.Delete(id);
        if (querySuccess == 1)
            return Ok("Record deleted with the id as " + id);
        else
            return Ok("Try again. No data deleted");
    }
}




