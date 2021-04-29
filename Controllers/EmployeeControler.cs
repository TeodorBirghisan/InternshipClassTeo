using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Controllers
{
    [Route("employee/[controller]")]
    [ApiController]
    public class EmployeeControler : ControllerBase
    {
        private readonly EmployeeDbService employeeDbService;

        public EmployeeControler(EmployeeDbService employeeDbService)
        {
            this.employeeDbService = employeeDbService;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeeDbService.GetEmployees();
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employeeDbService.GetEmployeeById(id);
        }

        [HttpPost]
        public Employee Post([FromBody] Employee employee)
        {
            employeeDbService.AddEmployee(employee);
            return employee;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeDbService.RemoveEmployee(id);
        }
    }
}
