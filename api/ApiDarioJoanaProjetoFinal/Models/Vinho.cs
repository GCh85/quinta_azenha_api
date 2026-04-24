using Microsoft.EntityFrameworkCore;

namespace ApiDarioJoanaProjetoFinal.Models;

public class Vinho
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public string? Tipo { get; set; }
    public string? Casta { get; set; }
    public int Ano { get; set; }
    [Precision(5, 2)]
    public decimal Alcool { get; set; }
    public string? Imagem { get; set; }
    public string? Harmonizacao { get; set; }
    public int Docura { get; set; }
    public int Acidez { get; set; }
    public int Corpo { get; set; }
    public bool Destaque { get; set; } = false;
}