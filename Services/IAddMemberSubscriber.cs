using InternshipClass.Models;

namespace InternshipClass.Services
{
    public interface IAddMemberSubscriber
    {
        void OnAddMember(Intern member);
    }
}