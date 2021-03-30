using System;
using System.Collections.Generic;

namespace InternshipClass.WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public double TemperatureC { get; set; }

        public string Summary { get; set; }

        public double TemperatureK { get; set; }
        //return TemperatureC + 273.15;
    }
}
