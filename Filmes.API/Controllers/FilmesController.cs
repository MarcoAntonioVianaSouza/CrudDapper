using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filmes.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok($"Serviço funcionando - Data e Hora Atual: {DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")}");
    }


}
