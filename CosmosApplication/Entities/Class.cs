using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class Class
    {
        public Class()
        {
            Sections = new HashSet<ClassSection>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }

        public ICollection<ClassSection> Sections { get; set; }
    }
}
