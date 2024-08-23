USE FILMESDB
GO

SELECT filmeId	as FilmeId,
	   f.nome	as Nome, 
	   f.ano	as Ano,
	   p.nome	as Nomeprodutora
FROM tb_filme as f (nolock)
INNER JOIN tb_produtora as p (nolock)
on f.produtoraId = p.produtoraId