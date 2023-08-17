using Microsoft.AspNetCore.Mvc;

namespace WebApplication6.Controllers;

[ApiController]
[Route("[controller]")]
public class NameController : ControllerBase
{
    private static readonly List<string> Summaries = new()
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet]
    public IEnumerable<string> Get()
    {
        return Summaries;
    }

    [HttpPost]
    public IActionResult Add([FromBody] AddDto dto)
    {
        if (!Summaries.Contains(dto.Name))
        {
            Summaries.Add(dto.Name);
        }

        return new ObjectResult(new
        {
            dto.Name
        });
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] string name)
    {
        if (!Summaries.Contains(name))
        {
            return NotFound();
        }

        Summaries.Remove(name);
        return new ObjectResult(name);
    }

    public class AddDto
    {
        public string Name { get; set; }
    }
}