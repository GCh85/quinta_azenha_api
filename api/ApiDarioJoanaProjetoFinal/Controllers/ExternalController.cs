using Microsoft.AspNetCore.Mvc;
using ApiDarioJoanaProjetoFinal.Services;

namespace ApiDarioJoanaProjetoFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExternalController : ControllerBase
{
    private readonly IImpostorService _impostor;

    public ExternalController(IImpostorService impostor)
    {
        _impostor = impostor;
    }

    [HttpGet("disponibilidade/{tipo}")]
    public async Task<IActionResult> Disponibilidade(string tipo)
    {
        var result = await _impostor.VerificarDisponibilidadeAsync(tipo);
        return Content(result, "application/json");
    }

    [HttpPost("pagamento")]
    public async Task<IActionResult> Pagamento([FromBody] PagamentoRequest req)
    {
        var result = await _impostor.ProcessarPagamentoAsync(req.Valor);
        return Content(result, "application/json");
    }
}

public class PagamentoRequest
{
    public decimal Valor { get; set; }
}