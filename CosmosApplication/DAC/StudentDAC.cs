using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.DAC
{
    public class StudentDAC
    {
        public int AddStudent(Student obj)
        {
            try
            {
                using (CosmosContext ctx = new CosmosContext())
                {
                    ctx.Students.Add(obj);
                    ctx.SaveChanges();
                    return obj.Id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Student> GetAllStudents()
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                return ctx.Students.ToList();
            }

        }

        public Student GetStudentById(int id)
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                return ctx.Students.Find(id);
            }

        }

        public void DeleteStudentById(int id)
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                Student std = ctx.Students.Find(id);
                ctx.Students.Remove(std);
            }

        }

        public bool UpdateFingerCode(int stdId, byte[] fingerData)
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                try
                {
                    var attachedEntity = ctx.Students.Find(stdId);

                    if (attachedEntity != null)
                    {
                        attachedEntity.FingerCode = fingerData;
                        var attachedEntry = ctx.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(attachedEntity);
                        ctx.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public byte[] GetFingerCodeById(int stdId)
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                try
                {
                    var attachedEntity = ctx.Students.Find(stdId);

                    if (attachedEntity != null)
                    {
                        return attachedEntity.FingerCode;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null; 
                }
            }

        }
    }
}
