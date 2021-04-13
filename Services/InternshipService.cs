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

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = GetMemberById(id);
            _internshipClass.Members.Remove(itemToBeDeleted);
        }

        public Intern AddMember(Intern member)
        {
            _internshipClass.Members.Add(member);
            return member;
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdated = GetMemberById(intern.Id);
            itemToBeUpdated.Name = intern.Name;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }

        public Intern GetMemberById(int id)
        {
            var member = _internshipClass.Members.Single(_ => _.Id == id);
            return member;
        }
    }
}
