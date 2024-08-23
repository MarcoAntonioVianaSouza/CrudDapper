USE FILMESDB
GO

select filmeId, f.nome, ano, f.produtoraId, p.nome as nomeprodutora
from tb_filme as f (nolock)
inner join tb_produtora as p (nolock)
on f.produtoraId = p.produtoraId

