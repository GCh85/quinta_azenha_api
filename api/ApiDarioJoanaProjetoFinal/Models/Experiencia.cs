using Microsoft.EntityFrameworkCore;

namespace ApiDarioJoanaProjetoFinal.Models;

public class Experiencia
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    [Precision(18, 2)]
    public decimal Preco { get; set; }
    public int DuracaoMinutos { get; set; }
    public int LotacaoMaxima { get; set; }
    public string? Epoca { get; set; }
    public string? Imagem { get; set; }
    public bool Ativa { get; set; } = true;
}