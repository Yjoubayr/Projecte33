using API.Classes.Model;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.historial;

[ApiController]
[Route("api/[controller]")]
public class HistorialController : ControllerBase
{
    private readonly HistorialService _HistorialService;

    public HistorialController(HistorialService historialService) =>
        _HistorialService = historialService;

    [HttpGet]
    public async Task<List<Historial>> Get() =>
        await _HistorialService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Historial>> Get(string id)
    {
        var historial = await _HistorialService.GetAsync(id);

        if (historial is null)
        {
            return NotFound();
        }

        return historial;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Historial newHistorial)
    {
        await _HistorialService.CreateAsync(newHistorial);

        return CreatedAtAction(nameof(Get), new { id = newHistorial.IDDispositiu }, newHistorial);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Historial updatedHistorial)
    {
        var historial = await _HistorialService.GetAsync(id);

        if (historial is null)
        {
            return NotFound();
        }

        updatedHistorial.IDDispositiu = historial.IDDispositiu;

        await _HistorialService.UpdateAsync(id, updatedHistorial);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var historial = await _HistorialService.GetAsync(id);

        if (historial is null)
        {
            return NotFound();
        }

        await _HistorialService.RemoveAsync(id);

        return NoContent();
    }
}