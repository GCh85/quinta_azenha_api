using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDarioJoanaProjetoFinal.Data;
using ApiDarioJoanaProjetoFinal.Models;
using ApiDarioJoanaProjetoFinal.Services;

namespace ApiDarioJoanaProjetoFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IImpostorService _impostor;

    public ReservasController(AppDbContext context, IImpostorService impostor)
    {
        _context = context;
        _impostor = impostor;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Reserva reserva)
    {
        if (string.IsNullOrEmpty(reserva.Nome))
            return BadRequest(new { error = "Nome e obrigatorio." });
        if (string.IsNullOrEmpty(reserva.Email) || !reserva.Email.Contains('@'))
            return BadRequest(new { error = "Email invalido." });
        if (string.IsNullOrEmpty(reserva.Assunto))
            return BadRequest(new { error = "Assunto e obrigatorio." });
        if (string.IsNullOrEmpty(reserva.Mensagem) || reserva.Mensagem.Trim().Length < 10)
            return BadRequest(new { error = "Mensagem deve ter pelo menos 10 caracteres." });

        reserva.Id = 0;
        reserva.Estado = "Pendente";
        reserva.CriadaEm = DateTime.UtcNow;

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        var disponibilidade = await _impostor.VerificarDisponibilidadeAsync(reserva.Assunto ?? "");

        return CreatedAtAction(nameof(GetById), new { id = reserva.Id }, new
        {
            reserva,
            disponibilidade
        });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var reservas = await _context.Reservas.OrderByDescending(r => r.CriadaEm).ToListAsync();
        return Ok(reservas);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null) return NotFound(new { error = "Reserva nao encontrada." });
        return Ok(reserva);
    }

    [HttpPut("{id}/estado")]
    [Authorize]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] string estado)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null) return NotFound(new { error = "Reserva nao encontrada." });

        if (estado != "Confirmada" && estado != "Cancelada" && estado != "Pendente")
            return BadRequest(new { error = "Estado invalido. Use: Pendente, Confirmada ou Cancelada." });

        reserva.Estado = estado;

        if (estado == "Confirmada")
        {
            // Simular pagamento via imposter (Mountebank)
            var pagamento = await _impostor.ProcessarPagamentoAsync(0);
        }

        await _context.SaveChangesAsync();
        return Ok(reserva);
    }
}