using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace InternshipClass.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Getting Weather Forecast for five days.
        /// </summary>
        /// <returns>Enumerablie of weather forecast objects.</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            List<WeatherForecast> weahterForecasts = FetchWeatherForecasts();

            return weahterForecasts.GetRange(1, 5);
        }

        [HttpGet("/forecast")]
        public List<WeatherForecast> FetchWeatherForecasts()
        {
            var latitude = double.Parse(configuration["WeatherForecast:Latitude"]);
            var longitude = double.Parse(configuration["WeatherForecast:Longitude"]);
            var aPIKey = configuration["WeatherForecast:APIKey"];

            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={aPIKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseToWeatherForecastList(response.Content);
        }

        [NonAction]
        public List<WeatherForecast> ConvertResponseToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"];
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            foreach (var item in jsonArray)
            {
                weatherForecasts.Add(new WeatherForecast
                {
                    Date = DateTimeConvertor.ConvertEpochToDateTime(item.Value<long>("dt")),
                    TemperatureK = item.SelectToken("temp").Value<double>("day"),
                    Summary = item.SelectToken("weather")[0].Value<string>("main"),
                });
            }

            return weatherForecasts;
        }
    }
}
