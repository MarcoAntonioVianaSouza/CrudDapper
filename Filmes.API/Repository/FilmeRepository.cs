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

    public Task<bool> AdicionarAsync(FilmeRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarAsync(FilmeRequest request, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<FilmeResponse> BuscarFilmeAsync(int id)
    {
        string sql = @"SELECT filmeId	as FilmeId,
	                f.nome	as Nome, 
	                f.ano	as Ano,
	                p.nome	as Produtora
                    FROM tb_filme as f (nolock)
                    INNER JOIN tb_produtora as p (nolock)
                    on f.produtoraId = p.produtoraId
                    WHERE f.filmedId = @Id";


        using (var con = new SqlConnection(connectionString))
        {
            return await con.QueryFirstOrDefaultAsync<FilmeResponse>(sql, new {Id = id});
        }
    }

    public async Task<IEnumerable<FilmeResponse>> BuscarFilmesAsync()
    {
        string sql = @"SELECT filmeId	as FilmeId,
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

    public Task<bool> DeletarAsync(int id)
    {
        throw new NotImplementedException();
    }
}
