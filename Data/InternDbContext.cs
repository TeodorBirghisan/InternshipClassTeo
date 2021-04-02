using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipClass.Data
{
    public class InternDbContext : DbContext
    {
        public InternDbContext(DbContextOptions<InternDbContext> options)
            : base(options)
        {
        }

        public DbSet<Intern> Interns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Intern>().ToTable("Intern");
        }
    }
}
