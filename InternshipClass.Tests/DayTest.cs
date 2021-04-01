using InternshipClass.Utilities;
using InternshipClass.WebAPI;
using InternshipClass.WebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using Xunit;

namespace InternshipClass.Tests
{
    public class DayTest
    {
        private IConfigurationRoot configuration;

        public DayTest()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        [Fact]
        public void CheckEpochConversion()
        {
            //Asume
            long ticks = 1617184800;

            //Act
            DateTime dateTime = DateTimeConvertor.ConvertEpochToDateTime(ticks);

            //Asert
            Assert.Equal(2021, dateTime.Year);
            Assert.Equal(03, dateTime.Month);
            Assert.Equal(31, dateTime.Day);
        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            //Asume
            //https://api.openweathermap.org/data/2.5/onecall?lat=45.75&lon=25.3333&exclude=hourly,minutely&appid=16ad7f7f931f63b0e8a7a494f7095d2c
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            //Act
            var weatherForcasts = weatherForecastController.FetchWeatherForecasts();

            //Assert
            Assert.Equal(8, weatherForcasts.Count);
        }

        [Fact]
        public void ConvertWeatherJsonToWeatherForecast()
        {      
            //Asume
            string content = GetStreamLines();
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            //Act
            var weatherForcasts = weatherForecastController.ConvertResponseToWeatherForecastList(content);
            WeatherForecast weatherForecastForTommorrow = weatherForcasts[1];

            //Assert
            //Forecast is volatile so make sure to change the value accordingly
            Assert.Equal(285.39, weatherForecastForTommorrow.TemperatureK);
        }

        private string GetStreamLines()
        {
            var assembly = this.GetType().Assembly;
            using var stream = assembly.GetManifestResourceStream("InternshipClass.Tests.WeatherForecast.json");
            StreamReader streamReader = new StreamReader(stream);

            var streamReaderLines = "";

            while (!streamReader.EndOfStream)
            {
                streamReaderLines += streamReader.ReadLine();
            }

            return streamReaderLines;
        }

        private WeatherForecastController InstantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }
    }
}
