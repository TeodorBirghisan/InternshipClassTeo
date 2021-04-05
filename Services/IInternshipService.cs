using System.Collections.Generic;
using InternshipClass.Models;

namespace InternshipClass.Services
{
    public interface IInternshipService
    {
        Intern AddMember(Intern member);

        void UpdateMember(Intern intern);

        IList<Intern> GetMembers();

        void RemoveMember(int index);
    }
}
