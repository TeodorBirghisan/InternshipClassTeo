﻿using System;
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
    }
}
