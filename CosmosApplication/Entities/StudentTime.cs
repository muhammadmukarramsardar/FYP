using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class StudentTime
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public Student Student { get; set; }
        public int? StudentId { get; set; }
        
    }
}
