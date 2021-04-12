using System;
using System.Collections.Generic;
using System.Linq;
using InternshipClass.Hubs;
using InternshipClass.Models;

namespace InternshipClass.Services
{
    public class InternshipService : IInternshipService
    {

        private readonly InternshipClassList _internshipClass = new ();
        private readonly List<IAddMemberSubscriber> subscribers;

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = _internshipClass.Members.Single(_ => _.Id == id);
            _internshipClass.Members.Remove(itemToBeDeleted);
        }

        public Intern AddMember(Intern member)
        {
            _internshipClass.Members.Add(member);
            subscribers.ForEach(subscriber => subscriber.OnAddMember(member));
            return member;
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdated = _internshipClass.Members.Single(_ => _.Id == intern.Id);
            itemToBeUpdated.Name = intern.Name;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }

        public void SubscribeToAddMember(IAddMemberSubscriber messageHub)
        {
            this.subscribers.Add(messageHub);
        }
    }
}
