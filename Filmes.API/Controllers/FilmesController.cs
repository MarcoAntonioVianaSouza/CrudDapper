using Filmes.API.Models;
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

    [HttpPost]
    public async Task<IActionResult> Post(FilmeRequest request)
    {
        if (string.IsNullOrEmpty(request.Nome) || request.Ano <=0 || request.ProdutoraId<=0)
        {
            return BadRequest("Informações inváldas");
        }
        var adicionado = await _filmeRepository.AdicionarAsync(request);
        return adicionado
            ? Ok("Filme adicionado com sucesso")
            : BadRequest("Erro ao adicionar filme");

    }

    [HttpPut("id")]
    public async Task<IActionResult> Put(FilmeRequest request, int id)
    {
        if (id <=0)
        {
            return BadRequest("Filme inválido");
        }

        var filme = await _filmeRepository.BuscarFilmeAsync(id);
        if (filme == null)
            return  NotFound("Filme não encontrado");


        if (string.IsNullOrEmpty(request.Nome) || request.Ano <= 0 || request.ProdutoraId <= 0)
        {
            return BadRequest("Informações inváldas");
        }
        var atualizado = await _filmeRepository.AtualizarAsync(request, id);
        return atualizado

            ? Ok("Filme atualizado com sucesso")
            : BadRequest("Erro ao atualizar filme");

    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Filme inválido");
        }

        var filme = await _filmeRepository.BuscarFilmeAsync(id);
        if (filme == null)
            return NotFound("Filme não encontrado");

              
        var deleteado = await _filmeRepository.DeletarAsync(id);
        return deleteado

            ? Ok("Filme removido com sucesso")
            : BadRequest("Erro ao remover filme");

    }

}
