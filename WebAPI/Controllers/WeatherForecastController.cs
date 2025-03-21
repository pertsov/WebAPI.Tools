using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/public/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Get weather forecast data.
        /// </summary>
        /// <returns>Returns 5 randomly generated weather forecast entries.</returns>
        [HttpGet] // GET localhost:<port>/api/public/weatherforecast
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeatherForecast>))]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
