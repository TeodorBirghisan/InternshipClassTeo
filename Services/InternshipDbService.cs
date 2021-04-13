using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Data;
using InternshipClass.Models;

namespace InternshipClass.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;
        private readonly List<IAddMemberSubscriber> subscribers;

        public InternshipDbService(InternDbContext db)
        {
            this.db = db;
            subscribers = new List<IAddMemberSubscriber>();
        }

        public Intern AddMember(Intern member)
        {
            db.Interns.AddRange(member);
            db.SaveChanges();
            subscribers.ForEach(subscriber => subscriber.OnAddMember(member));
            return member;
        }

        public void UpdateMember(Intern intern)
        {
            db.Interns.Update(intern);
            db.SaveChanges();
        }

        public IList<Intern> GetMembers()
        {
            var interns = db.Interns.ToList();
            return interns;
        }

        public void RemoveMember(int id)
        {
            var intern = GetMemberById(id);
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }

        public Intern GetMemberById(int id)
        {
            return db.Find<Intern>(id);
        }
    }
}
