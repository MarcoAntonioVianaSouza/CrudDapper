using Filmes.API.Models;

namespace Filmes.API.Repository;

public interface IFilmeRepository
{
    Task<IEnumerable<FilmeResponse>> BuscarFilmesAsync();
    Task<FilmeResponse> BuscarFilmeAsync(int id);
    Task<bool> AdicionarAsync(FilmeRequest request);
    Task<bool> AtualizarAsync(FilmeRequest request, int id);
    Task<bool> DeletarAsync(int id);
}
