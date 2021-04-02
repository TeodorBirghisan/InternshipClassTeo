﻿using System.Diagnostics;
using InternshipClass.Data;
using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternshipClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InternshipService internshipService;
        private readonly InternDbContext db;

        public HomeController(ILogger<HomeController> logger, InternshipService internshipService, InternDbContext db)
        {
            _logger = logger;
            this.internshipService = internshipService;
            this.db = db;
        }

        public IActionResult Index()
        {
            var interns = db.Interns;
            return View(interns);
        }

        public IActionResult Privacy()
        {
            return View(internshipService.GetClass());
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            internshipService.RemoveMember(index);
        }

        [HttpGet]
        public string AddMember(string member)
        {
            return internshipService.AddMember(member);
        }

        [HttpPut]
        public void UpdateMember(int index, string member)
        {
            internshipService.UpdateMember(index, member);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
