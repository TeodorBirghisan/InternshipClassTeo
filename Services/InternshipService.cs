using InternshipClass.Data;
using InternshipClass.Models;
using System.Collections.Generic;

namespace InternshipClass.Services
{
    public class InternshipService
    {
        private readonly InternshipClassList _internshipClassList = new InternshipClassList();

        public void RemoveMember(int index)
        {
            _internshipClassList.Members.RemoveAt(index);
        }

        public string AddMember(string member)
        {
            _internshipClassList.Members.Add(member);
            return member;
        }

        internal void UpdateMember(int index, string name)
        {
            _internshipClassList.Members[index] = name;
        }

        public InternshipClassList GetClass()
        {
            return _internshipClassList;
        }

    }
}
