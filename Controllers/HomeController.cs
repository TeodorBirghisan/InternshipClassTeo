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
