using InternshipClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Data
{
    public class SeedData
    {
        public static void Initialize(InternDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Interns.Any())
            {
                return;   // DB has been seeded
            }

            var interns = new Intern[]
            {
                new Intern { Id = 1, Name = "AAAAA", RegistrationDateTime = DateTime.Parse("2021-04-01") },
                new Intern { Id = 2, Name = "BBBBB", RegistrationDateTime = DateTime.Parse("2021-04-01") },
                new Intern { Id = 3, Name = "CCCCC", RegistrationDateTime = DateTime.Parse("2021-03-31") },
            };

            context.Interns.AddRange(interns);
            context.SaveChanges();
        }
    }
}
