namespace ApiDarioJoanaProjetoFinal.Services;

public interface IImpostorService
{
    Task<string> VerificarDisponibilidadeAsync(string tipoExperiencia);
    Task<string> ProcessarPagamentoAsync(decimal valor);
}