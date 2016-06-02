using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class ClassSection
    {
        public ClassSection()
        {
            Students = new HashSet<Student>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public Class Class { get; set; }
        public int? ClassId { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
