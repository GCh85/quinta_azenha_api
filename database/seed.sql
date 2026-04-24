-- =============================================
-- SEED DATA - Quinta da Azenha
-- Projeto Final UC00605
-- Base de Dados: QuintaAzenhaDB605
-- =============================================

USE QuintaAzenhaDB605;
GO

-- =============================================
-- VINHOS (5 vinhos)
-- =============================================

INSERT INTO Vinhos (Nome, Descricao, Tipo, Casta, Ano, Alcool, Imagem, Harmonizacao, Docura, Acidez, Corpo, Destaque)
VALUES
('Grande Reserva Vinhas Velhas', 'Nascido nas vinhas mais velhas. Estagio de 12 meses em barrica.', 'Branco', 'Arinto 100%', 2021, 13.5, 'QtaAzenhaGrdReserva.png', 'Peixe grelhado, Marisco', 20, 85, 75, 0),
('Grande Reserva Magnum', 'Em formato Magnum, 12 meses em carvalho frances.', 'Tinto', 'Touriga Nacional', 2014, 14.5, 'QtaAzenha_Magnum2014.png', 'Carne de vaca, Caca', 15, 65, 90, 0),
('Reserva Arinto-Chardonnay', 'Frescura do Arinto com elegancia do Chardonnay. Borbulhas finas.', 'Espumante', 'Arinto + Chardonnay', 2019, 12, 'QtaAzenhaBruto_2019.png', 'Sushi, Ostras', 10, 90, 50, 0),
('Trilogia de Arintos - Pelicular', 'Edicao numerada 3000 garrafas. Maceracao pelicular.', 'Branco', 'Arinto Pelicular', 2019, 13.5, 'QtaAzenha2019.png', 'Especiarias, Cozinha asiatica', 25, 80, 70, 0),
('Colheita Tardia Vinhas Velhas', 'Uma raridade que desafia o tempo. Dez anos em barricas de carvalho frances.', 'Branco', 'Arinto 100%', 2009, 11, 'QtaAzenhaTardia_2009.png', 'Queijo da Serra, Sobremesas de Ovo, Chocolate Negro', 65, 80, 85, 1);
GO

-- =============================================
-- EXPERIENCIAS (4 experiencias)
-- =============================================

INSERT INTO Experiencias (Nome, Descricao, Preco, DuracaoMinutos, LotacaoMaxima, Epoca, Imagem, Ativa)
VALUES
('Prova de Arinto', 'Viagem pelos solos de Bucelas atraves dos melhores Arintos.', 25.00, 90, 20, NULL, 'prova_vinhos.png', 1),
('Visita as Vinhas', 'Passeio guiado pelas vinhas centenarias de Arinto.', 15.00, 60, 15, NULL, 'QtaAzenha.png', 1),
('Workshop Vindima', 'Participe na colheita manual das uvas Arinto.', 40.00, 180, 12, 'Set/Out', 'prova_vinhos.png', 1),
('Jantar na Adega', 'Menu tradicional harmonizado com os nossos vinhos.', 60.00, 150, 10, NULL, 'jantar.png', 1);
GO

-- =============================================
-- UTILIZADOR ADMIN
-- Criar via API (Swagger): POST /api/auth/register
-- Email: admin@quintaazenha.pt
-- Password: Admin123!
-- =============================================

-- =============================================
-- VERIFICAR INSERCOES
-- =============================================

SELECT 'Vinhos:' AS Tabela, COUNT(*) AS Total FROM Vinhos
UNION ALL
SELECT 'Experiencias:', COUNT(*) FROM Experiencias;
GO

-- Ver detalhes
SELECT * FROM Vinhos;
SELECT * FROM Experiencias;
GO