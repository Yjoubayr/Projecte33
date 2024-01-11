using API.Classes.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.lletra;

[ApiController]
[Route("api/[controller]")]
public class CancoController : ControllerBase
{
    private readonly CancoService _CancoService;

    public CancoController(CancoService cancoService) =>
        _CancoService = cancoService;

    [HttpGet]
    public async Task<List<Canco>> Get() =>
        await _CancoService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Canco>> Get(string id)
    {
        var canco = await _CancoService.GetAsync(id);

        if (canco is null)
        {
            return NotFound();
        }

        return canco;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Canco newCanco)
    {
        await _CancoService.CreateAsync(newCanco);

        return CreatedAtAction(nameof(Get), new { id = newCanco._id }, newCanco);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Canco updatedCanco)
    {
        var canco = await _CancoService.GetAsync(id);

        if (canco is null)
        {
            return NotFound();
        }

        updatedCanco._id = canco._id;

        await _CancoService.UpdateAsync(id, updatedCanco);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var canco = await _CancoService.GetAsync(id);

        if (canco is null)
        {
            return NotFound();
        }

        await _CancoService.RemoveAsync(id);

        return NoContent();
    }
}