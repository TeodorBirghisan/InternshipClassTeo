using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public class Location
    {
        [JsonIgnore]
        public List<Intern> LocalInterns { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NativeName { get; internal set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
