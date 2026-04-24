using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace ApiDarioJoanaProjetoFinal.Cache;

public class CacheService
{
    private readonly IMemoryCache _l1;
    private readonly IDistributedCache _l2;
    private readonly TimeSpan _l1Duration = TimeSpan.FromSeconds(30);
    private readonly TimeSpan _l2Duration = TimeSpan.FromMinutes(5);

    public CacheService(IMemoryCache l1, IDistributedCache l2)
    {
        _l1 = l1;
        _l2 = l2;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        // L1: IMemoryCache (cache local - nanosegundos)
        if (_l1.TryGetValue(key, out T? cached))
            return cached;

        // L2: Redis (cache distribuido - milissegundos)
        var redisData = await _l2.GetStringAsync(key);
        if (redisData != null)
        {
            var value = JsonSerializer.Deserialize<T>(redisData);
            if (value != null)
            {
                // Repopular L1 para proximas leituras
                _l1.Set(key, value, _l1Duration);
                return value;
            }
        }

        return default;
    }

    public async Task SetAsync<T>(string key, T value)
    {
        // Guardar em L1 (local)
        _l1.Set(key, value, _l1Duration);

        // Guardar em L2 (Redis)
        var serialized = JsonSerializer.Serialize(value);
        var opts = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _l2Duration
        };
        await _l2.SetStringAsync(key, serialized, opts);
    }

    public async Task RemoveAsync(string key)
    {
        // Invalidar ambos os niveis
        _l1.Remove(key);
        await _l2.RemoveAsync(key);
    }
}