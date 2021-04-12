using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Models;
using InternshipClass.Services;
using Microsoft.AspNetCore.SignalR;

namespace InternshipClass.Hubs
{
    public class MessageHub : Hub, IAddMemberSubscriber
    {
        private readonly MessageService messageService;
        private readonly IInternshipService internshipService;

        public MessageHub(MessageService messageService, IInternshipService internshipService)
        {
            this.messageService = messageService;
            this.internshipService = internshipService;
            internshipService.SubscribeToAddMember(this);
        }

        public async void OnAddMember(Intern member)
        {
            await Clients.All.SendAsync("AddMemeber", member.Name, member.Id);
        }

        public async Task SendMessage(string user, string message)
        {
            messageService.AddMessage(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
