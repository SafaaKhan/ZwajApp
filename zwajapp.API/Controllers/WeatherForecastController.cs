using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;//?
using zwajapp.API.Data;
using zwajapp.API.Models;

namespace zwajapp.API.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DataContext _db;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext db)
    {
      _logger = logger;//??
      _db = db;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetWeathers()
    {
      // var rng = new Random();
      // return Enumerable.Range(1, 8).Select(index => new WeatherForecast //select?row??
      // {
      //   Date = DateTime.Now.AddDays(index),
      //   TemperatureC = rng.Next(-20, 55),
      //   Summary = Summaries[rng.Next(Summaries.Length)]
      // })
      // .ToArray();
      var weathers = await _db.WeatherForecasts.ToListAsync();
      return Ok(weathers);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWeather(int id)
    {
      var weather = await _db.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);
      return Ok(weather);

    }
  }
}
