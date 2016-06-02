using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class Student
    {
        public Student()
        {
            StudentTimes = new HashSet<StudentTime>();
            StudentGuardians = new HashSet<StudentGardians>();
        }
        public int Id { get; set; }
        public String RegNo { get; set; }
        public String Name { get; set; }
        public String FatherName { get; set; }
        public String FatherCNIC { get; set; }
        public ClassSection Section { get; set; }
        public int? SectionId { get; set; }
        public String RollNo { get; set; }
        public String Gender { get; set; }
        public String Religion { get; set; }
        public DateTime Dob { get; set; }
        public String HomeNumber { get; set; }
        public String FatherNo { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public Byte[] FingerCode { get; set; }
        public ICollection<StudentTime> StudentTimes { get; set; }
        public ICollection<StudentGardians> StudentGuardians { get; set; }
    }
}
