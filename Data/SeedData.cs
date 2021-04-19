﻿using InternshipClass.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Data
{
    public class SeedData
    {
        private static Location defaultLocation;

        public static void Initialize(InternDbContext context)
        {
            context.Database.Migrate();

            if (context.Interns.Any())
            {
                return;   // DB has been seeded
            }


            var locations = new Location[]
            {
                defaultLocation = new Location { Name = "Kyiv", NativeName = "Київ", Longitude = 30.5167, Latitude = 50.4333, },
                new Location { Name = "Brasov", NativeName = "Braşov", Longitude = 25.3333, Latitude = 45.75, },
            };

            context.Locations.AddRange(locations);

            var interns = new Intern[]
            {
                new Intern { Name = "AAAAA", RegistrationDateTime = DateTime.Parse("2021-04-01"), Location = defaultLocation },
                new Intern { Name = "BBBBB", RegistrationDateTime = DateTime.Parse("2021-04-01"), Location = defaultLocation },
                new Intern { Name = "CCCCC", RegistrationDateTime = DateTime.Parse("2021-03-31"), Location = defaultLocation },
            };

            context.Interns.AddRange(interns);

            context.SaveChanges();
        }
    }
}
