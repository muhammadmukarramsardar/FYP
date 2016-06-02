using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class Guardian
    {
        public Guardian()
        {
            GuardiansTimes = new HashSet<GuardianTime>();
            StudentGuardians = new HashSet<StudentGardians>();
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public String CNIC { get; set; }
        public String Relation { get; set; }
        public String Gender { get; set; }
        public String GuardianNo { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public Byte[] FingerCode { get; set; }
        public ICollection<StudentGardians> StudentGuardians { get; set; }
        public ICollection<GuardianTime> GuardiansTimes { get; set; }

    }
}
