using InternshipClass.Data;
using InternshipClass.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Services
{
    public class EmployeeDbService
    {
        private readonly InternDbContext db;
        private readonly IConfiguration configuration;

        public EmployeeDbService(InternDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public Employee AddEmployee(Employee employee)
        {
            db.Employees.AddRange(employee);
            db.SaveChanges();
            return employee;
        }

        public Employee GetEmployeeById(int id)
        {
            return db.Find<Employee>(id);
        }

        public IList<Employee> GetEmployees()
        {
            var employees = db.Employees.ToList();
            return employees;
        }
    }
}
