using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDarioJoanaProjetoFinal.Cache;
using ApiDarioJoanaProjetoFinal.Data;
using ApiDarioJoanaProjetoFinal.Models;

namespace ApiDarioJoanaProjetoFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VinhosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CacheService _cache;

    public VinhosController(AppDbContext context, CacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        const string key = "vinhos:todos";
        var cached = await _cache.GetAsync<List<Vinho>>(key);
        if (cached != null) return Ok(cached);

        var vinhos = await _context.Vinhos.ToListAsync();
        await _cache.SetAsync(key, vinhos);
        return Ok(vinhos);
    }

    [HttpGet("destaque")]
    public async Task<IActionResult> GetDestaque()
    {
        const string key = "vinhos:destaque";
        var cached = await _cache.GetAsync<Vinho>(key);
        if (cached != null) return Ok(cached);

        var vinho = await _context.Vinhos.FirstOrDefaultAsync(v => v.Destaque);
        if (vinho == null) return NotFound(new { error = "Nenhum vinho em destaque." });

        await _cache.SetAsync(key, vinho);
        return Ok(vinho);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var key = $"vinhos:{id}";
        var cached = await _cache.GetAsync<Vinho>(key);
        if (cached != null) return Ok(cached);

        var vinho = await _context.Vinhos.FindAsync(id);
        if (vinho == null) return NotFound(new { error = "Vinho nao encontrado." });

        await _cache.SetAsync(key, vinho);
        return Ok(vinho);
    }

    [HttpGet("tipo/{tipo}")]
    public async Task<IActionResult> GetByTipo(string tipo)
    {
        var key = $"vinhos:tipo:{tipo.ToLower()}";
        var cached = await _cache.GetAsync<List<Vinho>>(key);
        if (cached != null) return Ok(cached);

        var vinhos = await _context.Vinhos
            .Where(v => v.Tipo!.ToLower() == tipo.ToLower())
            .ToListAsync();
        await _cache.SetAsync(key, vinhos);
        return Ok(vinhos);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] Vinho vinho)
    {
        if (string.IsNullOrEmpty(vinho.Nome))
            return BadRequest(new { error = "Nome do vinho e obrigatorio." });

        vinho.Id = 0;
        _context.Vinhos.Add(vinho);
        await _context.SaveChangesAsync();

        await _cache.RemoveAsync("vinhos:todos");
        return CreatedAtAction(nameof(GetById), new { id = vinho.Id }, vinho);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] Vinho vinho)
    {
        var existing = await _context.Vinhos.FindAsync(id);
        if (existing == null) return NotFound(new { error = "Vinho nao encontrado." });

        existing.Nome = vinho.Nome;
        existing.Descricao = vinho.Descricao;
        existing.Tipo = vinho.Tipo;
        existing.Casta = vinho.Casta;
        existing.Ano = vinho.Ano;
        existing.Alcool = vinho.Alcool;
        existing.Imagem = vinho.Imagem;
        existing.Harmonizacao = vinho.Harmonizacao;
        existing.Docura = vinho.Docura;
        existing.Acidez = vinho.Acidez;
        existing.Corpo = vinho.Corpo;
        existing.Destaque = vinho.Destaque;

        await _context.SaveChangesAsync();

        // Invalidar cache
        await _cache.RemoveAsync($"vinhos:{id}");
        await _cache.RemoveAsync("vinhos:todos");
        await _cache.RemoveAsync("vinhos:destaque");

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var vinho = await _context.Vinhos.FindAsync(id);
        if (vinho == null) return NotFound(new { error = "Vinho nao encontrado." });

        _context.Vinhos.Remove(vinho);
        await _context.SaveChangesAsync();

        // Invalidar cache
        await _cache.RemoveAsync($"vinhos:{id}");
        await _cache.RemoveAsync("vinhos:todos");
        await _cache.RemoveAsync("vinhos:destaque");

        return NoContent();
    }
}