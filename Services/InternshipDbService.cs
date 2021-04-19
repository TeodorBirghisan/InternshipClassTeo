using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipClass.Data;
using InternshipClass.Models;
using Microsoft.Extensions.Configuration;

namespace InternshipClass.Services
{
    public class InternshipDbService : IInternshipService
    {
        private readonly InternDbContext db;
        private readonly List<IAddMemberSubscriber> subscribers;
        private IConfiguration configuration;
        private Location defaultLocation;

        public InternshipDbService(InternDbContext db, IConfiguration configuration)
        {
            this.db = db;
            subscribers = new List<IAddMemberSubscriber>();
            this.configuration = configuration;
        }

        public Intern AddMember(Intern member)
        {
            if (member.Location == null)
            {
                member.Location = GetDefaultLocation();
            }

            db.Interns.AddRange(member);
            db.SaveChanges();
            subscribers.ForEach(subscriber => subscriber.OnAddMember(member));
            return member;
        }

        private Location GetDefaultLocation()
        {
            if (defaultLocation == null)
            {
                var defaultLocationName = configuration["DefaultLocation"];
                defaultLocation = db.Locations.Where(_ => _.Name == defaultLocationName).OrderBy(_ => _.Id).FirstOrDefault();
            }

            return defaultLocation;
        }

        public void UpdateMember(Intern intern)
        {
            var itemToBeUpdated = GetMemberById(intern.Id);
            itemToBeUpdated.Name = intern.Name;

            if (intern.RegistrationDateTime > DateTime.MinValue)
            {
                itemToBeUpdated.RegistrationDateTime = DateTime.Now;
            }

            db.Interns.Update(itemToBeUpdated);
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
            if (intern == null) return;
            db.Remove<Intern>(intern);
            db.SaveChanges();
        }

        public Intern GetMemberById(int id)
        {
            var intern = db.Find<Intern>(id);
            db.Entry(intern).Reference(_ => _.Location).Load();
            return db.Find<Intern>(id);
        }

        public void UpdateLocation(int id, int locationId)
        {
            var intern = db.Find<Intern>(id);
            var location = db.Find<Location>(locationId);
            intern.Location = location;
            db.SaveChanges();
        }
    }
}
