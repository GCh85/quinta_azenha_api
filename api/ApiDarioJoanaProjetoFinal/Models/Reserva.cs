namespace ApiDarioJoanaProjetoFinal.Models;

public class Reserva
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Assunto { get; set; }
    public DateTime? DataPretendida { get; set; }
    public int NumeroPessoas { get; set; } = 1;
    public string? Mensagem { get; set; }
    public string Estado { get; set; } = "Pendente";
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
}