using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.DAC
{
    public class StudentGuardianDAC
    {
        public void AddStudentGuardian(StudentGardians studentGuardian)
        {
            using (CosmosContext ctx = new CosmosContext())
            {
                ctx.StudentsGuardians.Add(studentGuardian);
                ctx.SaveChanges();
            }
        }

        public List<Student> SelectAllStudentsByParentId(int id)
        {
            List<Student> students = new List<Student>();
            using (CosmosContext ctx = new CosmosContext())
            {
                List<StudentGardians> studentGuardians = ctx.StudentsGuardians.Where(m=>m.GuardianId == id).ToList();

                if (studentGuardians != null && studentGuardians.Count > 0)
                {
                    foreach (var stdGuardian in studentGuardians)
                    {
                        Student student = ctx.Students.Where(m => m.Id == (int)stdGuardian.StudentId).FirstOrDefault();

                        if (student != null)
                        {
                            students.Add(student);
                        }
                    }
                }

                return students;
            }
        }
    }
}
