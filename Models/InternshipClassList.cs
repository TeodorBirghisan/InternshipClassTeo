using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipClass.Models
{
    public class InternshipClassList
    {
        private readonly List<string> _members;

        public InternshipClassList()
        {
            _members = new List<string>
            {
                "Borys",
                "Liova",
                "Orest",
            };
        }

        public IList<string> Members => _members;
    }
}
