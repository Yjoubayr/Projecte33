using API.Classes.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LletraController : ControllerBase
{
    private readonly LletraService _LletraService;

    public LletraController(LletraService lletraService) =>
        _LletraService = lletraService;

    [HttpGet]
    public async Task<List<Lletra>> Get() =>
        await _LletraService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Lletra>> Get(string id)
    {
        var Lletra = await _LletraService.GetAsync(id);
        if (Lletra is null)
        {
            return NotFound();
        }
        
        return Lletra;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Lletra newLletra)
    {
        
        await _LletraService.CreateAsync(newLletra);

        return CreatedAtAction(nameof(Get), new { id = newLletra.IDLletra }, newLletra);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Lletra updatedLletra)
    {
        var Lletra = await _LletraService.GetAsync(id);

        if (Lletra is null)
        {
            return NotFound();
        }

        updatedLletra.IDLletra = Lletra.IDLletra;

        await _LletraService.UpdateAsync(id, updatedLletra);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var lletra = await _LletraService.GetAsync(id);

        if (lletra is null)
        {
            return NotFound();
        }

        await _LletraService.RemoveAsync(id);

        return NoContent();
    }
}