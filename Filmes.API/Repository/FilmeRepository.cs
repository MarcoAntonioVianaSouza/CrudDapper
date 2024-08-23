using Dapper;
using Filmes.API.Models;
using System.Data.SqlClient;

namespace Filmes.API.Repository;

public class FilmeRepository : IFilmeRepository
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;

    public FilmeRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetConnectionString("SqlConnection");
    }

    public IConfiguration Configuration { get; }

    public async Task<bool> AdicionarAsync(FilmeRequest request)
    {
        string @sql = @"INSERT INTO tb_filme(nome,ano,ProdutoraId) 
                       VALUES (@Nome, @Ano,@ProdutoraId)";


        using (var con = new SqlConnection(connectionString))
        {
            return await con.ExecuteAsync(sql, request) > 0;
        }

    }

    public async Task<bool> AtualizarAsync(FilmeRequest request, int id)
    {
        string @sql = @"UPDATE tb_filme 
                        SET nome = @Nome,
                            ano = @Ano,
                            produtoraId = @ProdutoraId
                        WHERE filmeId = @Id";

        var parametros = new DynamicParameters();
        parametros.Add("Ano", request.Ano);
        parametros.Add("Nome", request.Nome);
        parametros.Add("ProdutoraId", request.ProdutoraId);

        parametros.Add("Id", id);
        using (var con = new SqlConnection(connectionString))
        {
            return await con.ExecuteAsync(sql, parametros) > 0;
        }
    }

    public async Task<FilmeResponse> BuscarFilmeAsync(int id)
    {
        string sql = @"SELECT f.filmeId	as Id,
	                f.nome	as Nome, 
	                f.ano	as Ano,
	                p.nome	as Produtora
                    FROM tb_filme as f (nolock)
                    INNER JOIN tb_produtora as p (nolock)
                    on f.produtoraId = p.produtoraId
                    WHERE f.filmeId = @Id";


        using (var con = new SqlConnection(connectionString))
        {
            return await con.QueryFirstOrDefaultAsync<FilmeResponse>(sql, new {Id = id});
        }
    }

    public async Task<IEnumerable<FilmeResponse>> BuscarFilmesAsync()
    {
        string sql = @"SELECT filmeId as Id,
	                f.nome	as Nome, 
	                f.ano	as Ano,
	                p.nome	as Produtora
                    FROM tb_filme as f (nolock)
                    INNER JOIN tb_produtora as p (nolock)
                    on f.produtoraId = p.produtoraId";

        using (var con = new SqlConnection(connectionString))
        {
            return await con.QueryAsync<FilmeResponse>(sql);
        }
    }

    public async Task<bool> DeletarAsync(int id)
    {
        string @sql = @"DELETE FROM tb_filme WHERE filmeId = @Id";
               
        using (var con = new SqlConnection(connectionString))
        {
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
