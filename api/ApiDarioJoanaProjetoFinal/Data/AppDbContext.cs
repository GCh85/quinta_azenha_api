using Microsoft.EntityFrameworkCore;
using ApiDarioJoanaProjetoFinal.Models;

namespace ApiDarioJoanaProjetoFinal.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Vinho> Vinhos { get; set; }
    public DbSet<Experiencia> Experiencias { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vinho>().HasData(
            new Vinho { Id = 1, Nome = "Grande Reserva Vinhas Velhas", Descricao = "Nascido nas vinhas mais velhas. Estagio de 12 meses em barrica.", Tipo = "Branco", Casta = "Arinto 100%", Ano = 2021, Alcool = 13.5m, Imagem = "QtaAzenhaGrdReserva.png", Harmonizacao = "Peixe grelhado, Marisco", Docura = 20, Acidez = 85, Corpo = 75, Destaque = false },
            new Vinho { Id = 2, Nome = "Grande Reserva Magnum", Descricao = "Em formato Magnum, 12 meses em carvalho frances.", Tipo = "Tinto", Casta = "Touriga Nacional", Ano = 2014, Alcool = 14.5m, Imagem = "QtaAzenha_Magnum2014.png", Harmonizacao = "Carne de vaca, Caca", Docura = 15, Acidez = 65, Corpo = 90, Destaque = false },
            new Vinho { Id = 3, Nome = "Reserva Arinto-Chardonnay", Descricao = "Frescura do Arinto com elegancia do Chardonnay. Borbulhas finas.", Tipo = "Espumante", Casta = "Arinto + Chardonnay", Ano = 2019, Alcool = 12m, Imagem = "QtaAzenhaBruto_2019.png", Harmonizacao = "Sushi, Ostras", Docura = 10, Acidez = 90, Corpo = 50, Destaque = false },
            new Vinho { Id = 4, Nome = "Trilogia de Arintos - Pelicular", Descricao = "Edicao numerada 3000 garrafas. Maceracao pelicular.", Tipo = "Branco", Casta = "Arinto Pelicular", Ano = 2019, Alcool = 13.5m, Imagem = "QtaAzenha2019.png", Harmonizacao = "Especiarias, Cozinha asiatica", Docura = 25, Acidez = 80, Corpo = 70, Destaque = false },
            new Vinho { Id = 5, Nome = "Colheita Tardia Vinhas Velhas", Descricao = "Uma raridade que desafia o tempo. Dez anos em barricas de carvalho francs.", Tipo = "Branco", Casta = "Arinto 100%", Ano = 2009, Alcool = 11m, Imagem = "QtaAzenhaTardia_2009.png", Harmonizacao = "Queijo da Serra, Sobremesas de Ovo, Chocolate Negro", Docura = 65, Acidez = 80, Corpo = 85, Destaque = true }
        );

        modelBuilder.Entity<Experiencia>().HasData(
            new Experiencia { Id = 1, Nome = "Prova de Arinto", Descricao = "Viagem pelos solos de Bucelas atraves dos melhores Arintos.", Preco = 25m, DuracaoMinutos = 90, LotacaoMaxima = 20, Epoca = null, Imagem = "prova_vinhos.png", Ativa = true },
            new Experiencia { Id = 2, Nome = "Visita as Vinhas", Descricao = "Passeio guiado pelas vinhas centenarias de Arinto.", Preco = 15m, DuracaoMinutos = 60, LotacaoMaxima = 15, Epoca = null, Imagem = "QtaAzenha.png", Ativa = true },
            new Experiencia { Id = 3, Nome = "Workshop Vindima", Descricao = "Participe na colheita manual dasuvas Arinto.", Preco = 40m, DuracaoMinutos = 180, LotacaoMaxima = 12, Epoca = "Set/Out", Imagem = "prova_vinhos.png", Ativa = true },
            new Experiencia { Id = 4, Nome = "Jantar na Adega", Descricao = "Menu tradicional harmonizado com os nossos vinos.", Preco = 60m, DuracaoMinutos = 150, LotacaoMaxima = 10, Epoca = null, Imagem = "jantar.png", Ativa = true }
        );
    }
}