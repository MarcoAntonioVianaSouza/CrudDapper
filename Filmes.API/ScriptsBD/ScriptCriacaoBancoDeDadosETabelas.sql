CREATE DATABASE FilmesDB
GO

USE FilmesDB
CREATE TABLE tb_produtora
(
produtoraId	INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
nome VARCHAR(100) NOT NULL
);

CREATE TABLE tb_filme
(
filmeId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
nome varchar(100) not null,
ano int not null,
produtoraId int not null FOREIGN KEY REFERENCES tb_produtora(produtoraId)
)

