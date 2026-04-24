namespace ApiDarioJoanaProjetoFinal.Services;

public class ImpostorService : IImpostorService
{
    private readonly HttpClient _httpClient;

    public ImpostorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> VerificarDisponibilidadeAsync(string tipoExperiencia)
    {
        try
        {
            var encoded = Uri.EscapeDataString(tipoExperiencia);
            var response = await _httpClient.GetAsync($"/disponibilidade/{encoded}");
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"{{\"disponivel\": true, \"fonte\": \"fallback\", \"aviso\": \"{ex.Message}\"}}";
        }
    }

    public async Task<string> ProcessarPagamentoAsync(decimal valor)
    {
        try
        {
            var body = new StringContent(
                $"{{\"valor\": {valor}, \"moeda\": \"EUR\"}}",
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync("/pagamentos", body);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return $"{{\"transacaoId\": \"FALLBACK-000\", \"estado\": \"pendente\", \"aviso\": \"{ex.Message}\"}}";
        }
    }
}