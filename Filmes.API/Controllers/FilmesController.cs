using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    [HttpGet("validaservico")]
    public IActionResult Get()
    {
        return Ok($"Serviço funcionando - Data e Hora Atual: {DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}");
    }

    [HttpGet("calcularmedia/{nota1}/{nota2}")]
    public IActionResult CalcularMedia(double nota1, double nota2)
    {
        double media = (nota1 + nota2) / 2;
        return Ok($"A média do aluno é:{media:F2}");
    }

}
