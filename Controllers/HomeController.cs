using System;
using System.Diagnostics;
using System.Linq;
using InternshipClass.Data;
using InternshipClass.Hubs;
using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace InternshipClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService intershipService;
        private readonly MessageService messageService;
        private readonly IHubContext<MessageHub> hubContext;

        public HomeController(ILogger<HomeController> logger, IInternshipService intershipService, IHubContext<MessageHub> hubContext, MessageService messageService)
        {
            this.intershipService = intershipService;
            _logger = logger;
            this.hubContext = hubContext;
            this.messageService = messageService;
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            intershipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            intern.RegistrationDateTime = DateTime.Now;
            var newMember = intershipService.AddMember(intern);
            hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
            return newMember;
        }

        [HttpPut]
        public void UpdateMember(int index, string memberName)
        {
            Intern intern = new Intern();
            intern.Id = index;
            intern.Name = memberName;
            intern.RegistrationDateTime = DateTime.Now;
            intershipService.UpdateMember(intern);
        }


        public IActionResult Index()
        {
            var interns = intershipService.GetMembers();
            return View(interns);
        }

        public IActionResult Chat()
        {
            return View(messageService.GetAllMessages());
        }

        public IActionResult Privacy()
        {
            return View(intershipService.GetMembers());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
