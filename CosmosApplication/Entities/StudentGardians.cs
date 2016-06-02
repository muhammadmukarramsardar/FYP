using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class StudentGardians
    {
        public int Id { get; set; }
        public Guardian Guardian { get; set; }
        public int? GuardianId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }

    }
}
