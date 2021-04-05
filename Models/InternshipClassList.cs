using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public class InternshipClassList
    {
        private List<Intern> _members;

        public InternshipClassList()
        {
            _members = new List<Intern>
            {
                new Intern { Name = "Borys", RegistrationDateTime = DateTime.Parse("2021-04-01"), Id = 1 },
                new Intern { Name = "Liova", RegistrationDateTime = DateTime.Parse("2021-04-01"), Id = 2 },
                new Intern { Name = "Orest", RegistrationDateTime = DateTime.Parse("2021-03-31"), Id = 3 },
            };
        }

        public IList<Intern> Members
        {
            get { return _members; }
        }
    }
}
