# 🍷 API Quinta da Azenha

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat&logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![Redis](https://img.shields.io/badge/Redis-7-DC382D?style=flat&logo=redis)](https://redis.io/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat&logo=docker)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-Academic-blue?style=flat)](LICENSE)

> **Projeto Final UC00605 - Desenvolvimento Web Avançado**

API REST desenvolvida em ASP.NET Core 8 para a **Quinta da Azenha**, produtora de vinho Arinto DOC Bucelas. Esta API serve como backend para o website desenvolvido na UC00604, implementando gestão de vinhos, experiências enoturísticas e reservas.

---

## 📋 Índice

- [Funcionalidades](#-funcionalidades)
- [Arquitetura](#-arquitetura)
- [Tecnologias](#-tecnologias)
- [Endpoints](#-endpoints)
- [Início Rápido](#-início-rápido)
- [Configuração](#-configuração)
- [Desenvolvimento Local](#-desenvolvimento-local)
- [Testes](#-testes)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Segurança](#-segurança)
- [Cache e Performance](#-cache-e-performance)
- [Resiliência](#-resiliência)
- [Contribuição](#-contribuição)
- [Licença](#-licença)

---

## ✨ Funcionalidades

### Gestão de Vinhos
- 📖 Catálogo completo com perfil sensorial (doçura, acidez, corpo)
- 🍾 Vinhos em destaque
- 🔍 Filtro por tipo (Branco, Tinto, Espumante)
- ✏️ CRUD completo com autenticação JWT

### Experiências Enoturísticas
- 🎫 Prova de Arinto, Visita às Vinhas, Workshop Vindima, Jantar na Adega
- 💰 Gestão de preços e lotação
- 📅 Disponibilidade sazonal

### Sistema de Reservas
- 📝 Formulário público de contacto
- ✅ Validação coerente frontend/backend
- 🔔 Integração com serviço de disponibilidade (Mountebank)

### Autenticação
- 🔐 JWT Bearer Token
- 👤 BCrypt para hash de passwords
- 🛡️ Endpoints protegidos com `[Authorize]`

### Performance e Resiliência
- ⚡ Cache em dois níveis (L1: MemoryCache, L2: Redis)
- 🔄 Retry com backoff exponencial (Polly v8)
- 🔌 Circuit Breaker para chamadas externas

---

## 🏗️ Arquitetura

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                        FRONTEND (UC00604)                                   │
│                   Website Quinta da Azenha                                  │
│                  HTML + Bootstrap + JavaScript                              │
└───────────────────────────────────┬─────────────────────────────────────────┘
                                    │ HTTP/JSON
                                    ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                         API REST (.NET 8)                                   │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐         │
│  │ Controllers │  │  Services   │  │    Cache    │  │  Security   │         │
│  │             │  │             │  │             │  │             │         │
│  │ • Vinhos    │  │ Impostor    │  │ L1: Memory  │  │ JWT Bearer  │         │
│  │ • Experien. │  │ Service     │  │ L2: Redis   │  │ BCrypt      │         │
│  │ • Reservas  │  │             │  │             │  │             │         │
│  │ • Auth      │  │             │  │             │  │             │         │
│  │ • External  │  │             │  │             │  │             │         │
│  └─────────────┘  └─────────────┘  └─────────────┘  └─────────────┘         │
│                                                                             │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐                          │
│  │   Models    │  │   Data      │  │   Polly     │                          │
│  │   (DTOs)    │  │ DbContext   │  │ Resilience  │                          │
│  └─────────────┘  └─────────────┘  └─────────────┘                          │
└───────────────────────────────┬─────────────────────────────────────────────┘
                                │
            ┌───────────────────┼───────────────────┐
            ▼                   ▼                   ▼
    ┌───────────────┐   ┌───────────────┐   ┌─────────────────┐
    │  SQL Server   │   │    Redis      │   │  Mountebank     │
    │   (Dados)     │   │  (Cache L2)   │   │  (Imposter)     │
    │               │   │               │   │                 │
    │ • Users       │   │ • vinhos:*    │   │ /disponibilidade│
    │ • Vinhos      │   │ • experien:*  │   │ /pagamentos     │
    │ • Experiencias│   │               │   │                 │
    │ • Reservas    │   │               │   │                 │
    └───────────────┘   └───────────────┘   └─────────────────┘
```

---

## 🛠️ Tecnologias

| Tecnologia | Versão | Finalidade |
|------------|--------|------------|
| **ASP.NET Core** | 8.0 | Framework da API |
| **Entity Framework Core** | 8.0 | ORM (Code-First) |
| **SQL Server** | 2022 | Base de dados relacional |
| **Redis** | 7 | Cache distribuído (L2) |
| **JWT Bearer** | - | Autenticação stateless |
| **BCrypt.Net** | 4.0.2 | Hash de passwords |
| **Polly** | 8.0 | Resiliência (Retry, Circuit Breaker) |
| **Mountebank** | latest | Imposter para serviços externos |
| **Docker Compose** | 3.8 | Orquestração de containers |
| **Swagger/OpenAPI** | 6.9 | Documentação interativa |

---

## 🚀 Endpoints

### Autenticação

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|:----:|
| `POST` | `/api/auth/register` | Registar administrador | ❌ |
| `POST` | `/api/auth/login` | Login (retorna JWT) | ❌ |

### Vinhos

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|:----:|
| `GET` | `/api/vinhos` | Listar todos os vinhos | ❌ |
| `GET` | `/api/vinhos/{id}` | Obter vinho por ID | ❌ |
| `GET` | `/api/vinhos/destaque` | Vinho em destaque | ❌ |
| `GET` | `/api/vinhos/tipo/{tipo}` | Filtrar por tipo | ❌ |
| `POST` | `/api/vinhos` | Criar vinho | ✅ |
| `PUT` | `/api/vinhos/{id}` | Atualizar vinho | ✅ |
| `DELETE` | `/api/vinhos/{id}` | Eliminar vinho | ✅ |

### Experiências

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|:----:|
| `GET` | `/api/experiencias` | Listar experiências ativas | ❌ |
| `GET` | `/api/experiencias/{id}` | Obter experiência por ID | ❌ |
| `POST` | `/api/experiencias` | Criar experiência | ✅ |
| `DELETE` | `/api/experiencias/{id}` | Desativar experiência | ✅ |

### Reservas

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|:----:|
| `POST` | `/api/reservas` | Criar reserva (formulário público) | ❌ |
| `GET` | `/api/reservas` | Listar reservas | ✅ |
| `GET` | `/api/reservas/{id}` | Obter reserva por ID | ✅ |
| `PUT` | `/api/reservas/{id}/estado` | Atualizar estado | ✅ |

### Serviços Externos (Mountebank)

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|:----:|
| `GET` | `/api/external/disponibilidade/{tipo}` | Verificar disponibilidade | ❌ |
| `POST` | `/api/external/pagamento` | Processar pagamento | ❌ |

---

## ⚡ Início Rápido

### Pré-requisitos

- **Docker Desktop** (recomendado)
- .NET 8 SDK (para desenvolvimento local)
- Git

### Executar com Docker (Recomendado)

```bash
# 1. Clonar o repositório
git clone https://github.com/teu-usuario/api-dariojoana-projeto-final.git
cd api-dariojoana-projeto-final

# 2. Iniciar todos os serviços
docker-compose up --build

# 3. Aceder à API
# Swagger: http://localhost:5169
# Redis: localhost:6379
# Mountebank Admin: http://localhost:2525
# Mountebank API: http://localhost:4546
```

### Executar Localmente (Visual Studio)

```bash
# 1. Iniciar serviços de suporte
docker-compose up -d sqlserver redis mountebank

# 2. Abrir solução no Visual Studio
# api/ApiDarioJoanaProjetoFinal.sln

# 3. Pressione F5 para executar

# 4. A API estará disponível em:
# https://localhost:7000 ou http://localhost:5169
```

### Criar Utilizador Admin

```bash
# Via Swagger ou Postman
POST /api/auth/register
{
  "email": "admin@quintaazenha.pt",
  "password": "Admin123!"
}
```

---

## ⚙️ Configuração

### Variáveis de Ambiente

| Variável | Descrição | Default |
|----------|-----------|---------|
| `ConnectionStrings__DefaultConnection` | Connection string SQL Server | Ver `appsettings.json` |
| `JwtSettings__Key` | Chave secreta JWT (32+ chars) | - |
| `JwtSettings__Issuer` | Emissor do token | `ApiQuintaAzenha` |
| `JwtSettings__Audience` | Audiência do token | `QuintaAzenhaClients` |
| `Redis__ConnectionString` | Connection string Redis | `localhost:6379` |
| `ImpostorUrl` | URL do Mountebank | `http://localhost:4546` |

### Credenciais Docker

| Serviço | Utilizador | Password |
|---------|------------|----------|
| SQL Server | `sa` | `QuintaAzenha2026!` |
| Redis | - | - |
| Mountebank | - | - |

---

## 💻 Desenvolvimento Local

### Pré-requisitos Adicionais

- Visual Studio 2022 (17.8+)
- SQL Server LocalDB (incluído no VS)
- Redis (opcional, pode usar Docker apenas para Redis)

### Passos

```bash
# 1. Abrir a solução
start api/ApiDarioJoanaProjetoFinal.sln

# 2. Restaurar pacotes NuGet
dotnet restore

# 3. Executar migrações (se necessário)
dotnet ef database update

# 4. Iniciar apenas Redis e Mountebank
docker-compose up -d redis mountebank

# 5. Executar a API (F5 no Visual Studio)
```

### Configuração sem Docker

Editar `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=QuintaAzenhaDB605;Trusted_Connection=True;"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "ImpostorUrl": "http://localhost:4546"
}
```

---

## 🧪 Testes

### Swagger

Aceder a `http://localhost:5169/swagger`

1. **Registar utilizador:** `POST /api/auth/register`
2. **Fazer login:** `POST /api/auth/login` → copiar token
3. **Autorizar:** Clicar em 🔓 **Authorize** → `Bearer {token}`
4. **Testar endpoints protegidos:** Os endpoints com cadeado 🔒 requerem autenticação

### Postman

Importar a coleção em `postman/collection.json`:

```bash
# 1. Abrir Postman
# 2. Import → Upload Files → postman/collection.json
# 3. Executar requests pela ordem:
#    - Auth: Register → Login (guarda token automaticamente)
#    - Vinhos: GET lista
#    - Reservas: POST criar reserva
#    - External: GET disponibilidade
```

### Verificar Cache Redis

```bash
# Conectar ao Redis
docker exec -it <redis_container> redis-cli

# Listar todas as chaves
KEYS *

# Ver conteúdo de uma chave
GET "vinhos:todos"

# Ver tempo de vida
TTL "vinhos:todos"
```

---

## 📁 Estrutura do Projeto

```
api-dariojoana-projeto-final/
├── api/
│   ├── ApiDarioJoanaProjetoFinal.sln
│   └── ApiDarioJoanaProjetoFinal/
│       ├── Controllers/
│       │   ├── AuthController.cs          # Autenticação JWT
│       │   ├── VinhosController.cs         # CRUD Vinhos
│       │   ├── ExperienciasController.cs   # CRUD Experiências
│       │   ├── ReservasController.cs       # Reservas
│       │   └── ExternalController.cs       # Mountebank integration
│       ├── Models/
│       │   ├── Vinho.cs
│       │   ├── Experiencia.cs
│       │   ├── Reserva.cs
│       │   ├── User.cs
│       │   ├── LoginRequest.cs
│       │   └── LoginResponse.cs
│       ├── Data/
│       │   └── AppDbContext.cs              # EF Core DbContext + Seed
│       ├── Services/
│       │   ├── IImpostorService.cs
│       │   └── ImpostorService.cs           # Mountebank client
│       ├── Cache/
│       │   └── CacheService.cs             # L1 + L2 Cache
│       ├── Program.cs                       # Startup + DI
│       ├── appsettings.json
│       └── Dockerfile
├── database/
│   ├── schema.sql                          # Estrutura das tabelas
│   └── seed.sql                             # Dados iniciais
├── frontend/
│   ├── index.html                           # Website UC00604
│   ├── css/
│   ├── js/
│   └── pag/
├── imposter/
│   └── mountebank.json                      # Configuração Mountebank
├── postman/
│   └── collection.json                       # Coleção Postman
├── docker-compose.yml
├── .env.example
├── README.md
└── RELATORIO.md
```

---

## 🔒 Segurança

### Autenticação

- **JWT Bearer Token** com expiração de 60 minutos
- **BCrypt** para hash de passwords (salt automático)
- **HMAC-SHA256** para assinatura do token

### Validação

- Validação em ambas as camadas (frontend + backend)
- Nome: obrigatório
- Email: formato válido (regex)
- Mensagem: mínimo 10 caracteres

### CORS

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
```

### HTTPS

Redirecionamento automático em produção via `app.UseHttpsRedirection()`.

---

## ⚡ Cache e Performance

### Cache em Dois Níveis

| Nível | Tecnologia | Duração | Latência |
|-------|------------|---------|----------|
| L1 | IMemoryCache | 30s | ~1ms (nanosegundos) |
| L2 | Redis | 5min | ~1-5ms |

### Padrão Cache-Aside

```csharp
public async Task<T?> GetAsync<T>(string key)
{
    // L1: IMemoryCache
    if (_l1.TryGetValue(key, out T? cached))
        return cached;

    // L2: Redis
    var redisData = await _l2.GetStringAsync(key);
    if (redisData != null)
    {
        var value = JsonSerializer.Deserialize<T>(redisData);
        _l1.Set(key, value, _l1Duration); // Repopula L1
        return value;
    }

    return default; // MISS total → ir à BD
}
```

### Invaliação

Dados são invalidados em operações de escrita:

```csharp
await _cache.RemoveAsync($"vinhos:{id}");
await _cache.RemoveAsync("vinhos:todos");
```

---

## 🔄 Resiliência

### Polly v8 - Standard Resilience Handler

```csharp
.AddStandardResilienceHandler(options =>
{
    // Retry: 3 tentativas com backoff exponencial
    options.Retry.MaxRetryAttempts = 3;
    options.Retry.Delay = TimeSpan.FromSeconds(1);

    // Circuit Breaker: abre após 50% de falhas
    options.CircuitBreaker.FailureRatio = 0.5;
    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
    options.CircuitBreaker.MinimumThroughput = 5;
    options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(30);
});
```

### Comportamento

```
Request → Retry (3x) → Se falhar todas:
    ↓
    └─→ Circuit Breaker abre (30s)
        └─=> Durante este período: Fallback (resposta pré-definida)
```

---

## 🤝 Contribuição

1. Fork o repositório
2. Criar branch para feature (`git checkout -b feature/nova-funcionalidade`)
3. Commit das alterações (`git commit -m 'Adicionar nova funcionalidade'`)
4. Push para o branch (`git push origin feature/nova-funcionalidade`)
5. Abrir Pull Request

### Convenções de Código

- Seguir [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Usar **Async/Await** em operações de I/O
- Validar inputs nos controllers
- Documentar endpoints com `[ProducesResponseType]`

---

## 📄 Licença

Projeto académico desenvolvido para a **UC00605 - Desenvolvimento Web Avançado**.

---

<div align="center">

**Made with ❤️ by GCh85 for UC00605**

[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=flat&logo=bootstrap)](https://getbootstrap.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=flat&logo=swagger)](https://swagger.io/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=flat&logo=dotnet)](https://docs.microsoft.com/ef/core/)

</div>