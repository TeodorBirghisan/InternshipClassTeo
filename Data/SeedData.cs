using InternshipClass.Models;
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

            if (!context.Locations.Any())
            {
                var locations = new Location[]
                {
                defaultLocation = new Location { Name = "Kyiv", NativeName = "Київ", Longitude = 30.5167, Latitude = 50.4333, },
                new Location { Name = "Brasov", NativeName = "Braşov", Longitude = 25.3333, Latitude = 45.75, },
                };
                context.Locations.AddRange(locations);
                context.SaveChanges();
            }

            if (!context.Interns.Any())
            {
                var interns = new Intern[]
                {
                        new Intern { Name = "AAAAA", RegistrationDateTime = DateTime.Parse("2021-04-01"), Location = defaultLocation },
                        new Intern { Name = "BBBBB", RegistrationDateTime = DateTime.Parse("2021-04-01"), Location = defaultLocation },
                        new Intern { Name = "CCCCC", RegistrationDateTime = DateTime.Parse("2021-03-31"), Location = defaultLocation },
                };

                context.Interns.AddRange(interns);

                context.SaveChanges();
            }

            if (!context.Projects.Any())
            {
                var projects = new Project[]
                {
                    new Project
                    {
                        Name = "Build a bot",
                        StartDate = DateTime.Parse("2020-09-01"),
                        Interns = context.Interns.ToList(),
                        Url = "https://gitlab.com/borysl/build-a-bot",
                        IsPublished = false,
                    },
                    new Project
                    {
                        Name = "Multiplication table",
                        StartDate = DateTime.Parse("2020-02-01"),
                        Interns = new Intern[]
                        {
                            context.Interns.Single(_ => _.Name == "BBBBB"),
                        },
                        Url = "https://mtab.herokuapp.com/",
                        IsPublished = true,
                    },
                };
                context.Projects.AddRange(projects);
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                var employees = new Employee[]
                {
                    new Employee { Id = 1, FirstName = "Teodor", LastName="Birghisan", Email="birghisanteodor@yahoo.com", Gender="Masculin", Birthdate= DateTime.Parse("1999-05-07"), Picture="pic.jpg" },
                    new Employee { Id = 2, FirstName = "Teodor", LastName="Birghisan", Email="birghisanteodor@yahoo.com", Gender="Masculin", Birthdate= DateTime.Parse("1999-05-07") , Picture="pic.jpg" },
                    new Employee { Id = 3, FirstName = "Teodor", LastName ="Birghisan", Email="birghisanteodor@yahoo.com", Gender="Masculin", Birthdate= DateTime.Parse("1999-05-07") , Picture="pic.jpg" },
                };

                context.Employees.AddRange(employees);

                context.SaveChanges();
            }
        }
    }
}
