using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.DAC
{
    public class ClassDAC
    {
        public List<Class> GetAllClasses()
        {
            using (CosmosContext ctx = new CosmosContext())
            {
                return ctx.Classes.ToList();
            }
        }
    }
}
