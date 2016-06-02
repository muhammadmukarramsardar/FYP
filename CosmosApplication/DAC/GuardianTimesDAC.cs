using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.DAC
{
    public class GuardianTimesDAC
    {
        public GuardianTime AddGuardianTime(GuardianTime time)
        {
            using (CosmosContext ctx = new CosmosContext())
            {
                ctx.GuardianTimes.Add(time);
                ctx.SaveChanges();
                return time;
            }
        }

        public List<Guardian> FindLateGuardianTime()
        {
            using (CosmosContext ctx = new CosmosContext())
            {
                var list = ctx.Guardians.Include("GuardiansTimes").ToList();
                List<Guardian> guardiansLate = new List<Guardian>();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.GuardiansTimes != null && item.GuardiansTimes.Count > 0)
                        {
                            bool found = false;
                            foreach (var time in item.GuardiansTimes)
                            {
                                if (time.DateTime.Date == DateTime.Today.Date)
                                {
                                    found = true;
                                }
                            }

                            if (!found)
                            {
                                guardiansLate.Add(item);
                            }
                        }
                        else
                        {
                            guardiansLate.Add(item);
                        }
                    }
                }
                return guardiansLate;
            }
        }
    }
}
