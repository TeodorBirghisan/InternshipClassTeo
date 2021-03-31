using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Utilities;
using Microsoft.AspNetCore.Mvc;
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
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Getting Weather Forecast for five days.
        /// </summary>
        /// <returns>Enumerablie of weather forecast objects.</returns>

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var latitude = 45.75;
            var longitude = 25.3333;
            var aPIKey = "e63d79372e7668d3ac31be36bd82af21";
            List<WeatherForecast> weahterForecasts = FetchWeatherForecasts(latitude, longitude, aPIKey);

            return weahterForecasts.GetRange(1, 5);
        }

        public List<WeatherForecast> FetchWeatherForecasts(double latitude, double longitude, string aPIKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=hourly,minutely&appid={aPIKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseToWeatherForecastList(response.Content);
        }

        public List<WeatherForecast> ConvertResponseToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var jsonArray = json["daily"];
            List<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
            foreach (var item in jsonArray)
            {
                WeatherForecast obj = new WeatherForecast();
                obj.Date = DateTimeConvertor.ConvertEpochToDateTime(item.Value<long>("dt"));
                obj.TemperatureK = item.SelectToken("temp").Value<double>("day");
                obj.Summary = item.SelectToken("weather")[0].Value<string>("main");

                try
                {
                    weatherForecasts.Add(obj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return weatherForecasts;
        }
    }
}
