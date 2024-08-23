using Filmes.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    private readonly IFilmeRepository _filmeRepository;
    public FilmesController(IFilmeRepository filmeRepository)
    {
        _filmeRepository = filmeRepository;
    }

    [HttpGet("validaservico")]
    public IActionResult GetValidarServico()
    {
        return Ok($"Serviço funcionando - Data e Hora Atual: {DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}");
    }

    [HttpGet("calcularmedia/{nota1}/{nota2}")]
    public IActionResult CalcularMedia(double nota1, double nota2)
    {
        double media = (nota1 + nota2) / 2;
        return Ok($"A média do aluno é:{media:F2}");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var filmes = await _filmeRepository.BuscarFilmesAsync();
        return filmes.Any() ? Ok(filmes) : NoContent();

    }

    [HttpGet("id")]
    public async Task<IActionResult> Get(int id)
    {
        var filme = await _filmeRepository.BuscarFilmeAsync(id);
        return filme !=null
            ? Ok(filme) 
            : NotFound("Filme não encontrado");
    }


}
