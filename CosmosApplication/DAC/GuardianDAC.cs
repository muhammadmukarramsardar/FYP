using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.DAC
{
    public class GuardianDAC
    {
        public Guardian AddGuardian(Guardian obj)
        {
            try
            {
                using (CosmosContext ctx = new CosmosContext())
                {
                    ctx.Guardians.Add(obj);
                    ctx.SaveChanges();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Guardian SelectGuardianByCNIC(string cnic)
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                return (from g in ctx.Guardians where g.CNIC.Equals(cnic, StringComparison.OrdinalIgnoreCase) select g).FirstOrDefault();
            }

        }

        public void Update(Guardian guardian)
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                Guardian attachedEnttiy = ctx.Guardians.Find(guardian.Id);

                if (attachedEnttiy != null)
                {
                    try
                    {
                        attachedEnttiy.Address = guardian.Address;
                        attachedEnttiy.Email = guardian.Email;
                        attachedEnttiy.GuardianNo = guardian.GuardianNo;
                        attachedEnttiy.Name = guardian.Name;
                        attachedEnttiy.Relation = guardian.Relation;

                        var attachedEntry = ctx.Entry(attachedEnttiy);
                        attachedEntry.CurrentValues.SetValues(attachedEnttiy);
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                   
                }
            }

        }

        public List<Guardian> SelectAllGuardians()
        {

            using (CosmosContext ctx = new CosmosContext())
            {
                try
                {
                    return ctx.Guardians.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
    }
}
