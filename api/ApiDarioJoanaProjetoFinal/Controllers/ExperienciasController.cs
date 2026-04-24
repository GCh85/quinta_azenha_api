using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDarioJoanaProjetoFinal.Cache;
using ApiDarioJoanaProjetoFinal.Data;
using ApiDarioJoanaProjetoFinal.Models;

namespace ApiDarioJoanaProjetoFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperienciasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CacheService _cache;

    public ExperienciasController(AppDbContext context, CacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        const string key = "experiencias:ativas";
        var cached = await _cache.GetAsync<List<Experiencia>>(key);
        if (cached != null) return Ok(cached);

        var lista = await _context.Experiencias.Where(e => e.Ativa).ToListAsync();
        await _cache.SetAsync(key, lista);
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var key = $"experiencias:{id}";
        var cached = await _cache.GetAsync<Experiencia>(key);
        if (cached != null) return Ok(cached);

        var exp = await _context.Experiencias.FindAsync(id);
        if (exp == null) return NotFound(new { error = "Experiencia nao encontrada." });

        await _cache.SetAsync(key, exp);
        return Ok(exp);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] Experiencia exp)
    {
        if (string.IsNullOrEmpty(exp.Nome))
            return BadRequest(new { error = "Nome da experiencia e obrigatorio." });

        exp.Id = 0;
        _context.Experiencias.Add(exp);
        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("experiencias:ativas");
        return CreatedAtAction(nameof(GetById), new { id = exp.Id }, exp);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var exp = await _context.Experiencias.FindAsync(id);
        if (exp == null) return NotFound(new { error = "Experiencia nao encontrada." });

        exp.Ativa = false;
        await _context.SaveChangesAsync();
        await _cache.RemoveAsync("experiencias:ativas");
        await _cache.RemoveAsync($"experiencias:{id}");
        return NoContent();
    }
}