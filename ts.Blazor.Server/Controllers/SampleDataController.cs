<<<<<<< HEAD
﻿using ts.Blazor.Shared;
=======
﻿
>>>>>>> a906ea0910cc0354a1eaf317acbca740793f1aeb
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using ts.Domain;
using ts.Domain.Entities;
>>>>>>> a906ea0910cc0354a1eaf317acbca740793f1aeb

namespace ts.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
<<<<<<< HEAD
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
=======
        [HttpGet("[action]")]
        public IEnumerable<SecurityScope> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new SecurityScope
            {
                Name = "Name",
>>>>>>> a906ea0910cc0354a1eaf317acbca740793f1aeb
            });
        }
    }
}
