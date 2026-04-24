CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Email NVARCHAR(150) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255),
    Role NVARCHAR(50) DEFAULT 'Admin',
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE Vinhos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150),
    Descricao NVARCHAR(500),
    Tipo NVARCHAR(50),
    Casta NVARCHAR(100),
    Ano INT,
    Alcool DECIMAL(4,1),
    Imagem NVARCHAR(100),
    Harmonizacao NVARCHAR(200),
    Docura INT,
    Acidez INT,
    Corpo INT,
    Destaque BIT DEFAULT 0
);

CREATE TABLE Experiencias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(150),
    Descricao NVARCHAR(500),
    Preco DECIMAL(8,2),
    DuracaoMinutos INT,
    LotacaoMaxima INT,
    Epoca NVARCHAR(50) NULL,
    Imagem NVARCHAR(100),
    Ativa BIT DEFAULT 1
);

CREATE TABLE Reservas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100),
    Email NVARCHAR(150),
    Telefone NVARCHAR(20) NULL,
    Assunto NVARCHAR(100),
    DataPretendida DATETIME2 NULL,
    NumeroPessoas INT DEFAULT 1,
    Mensagem NVARCHAR(1000),
    Estado NVARCHAR(50) DEFAULT 'Pendente',
    CriadaEm DATETIME2 DEFAULT GETUTCDATE()
);