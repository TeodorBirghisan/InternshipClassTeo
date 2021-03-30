using InternshipClass.Utilities;
using InternshipClass.WebAPI;
using InternshipClass.WebAPI.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace InternshipClass.Tests
{
    public class DayTest
    {
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
            var latitude = 45.75;
            var longitude = 25.3333;
            var APIKey = "e63d79372e7668d3ac31be36bd82af21";
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger);

            //Act
            var weatherForcasts = weatherForecastController.FetchWeatherForecasts(latitude,longitude,APIKey);
            WeatherForecast weatherForecastForTommorrow = weatherForcasts[1];

            //Assert
            //Forecast is volatile so make sure to change the value accordingly
            Assert.Equal(284.07, weatherForecastForTommorrow.TemperatureK);

        }
    }
}
