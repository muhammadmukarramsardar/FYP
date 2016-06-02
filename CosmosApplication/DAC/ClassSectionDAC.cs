using CosmosApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.DAC
{
    public class ClassSectionDAC
    {
        public List<ClassSection> GetAllSectionsByClassId(int id)
        {
            using (CosmosContext ctx = new CosmosContext())
            {
                // LINQ (Language Integrated Query)
                return (from c in ctx.ClassSections where c.ClassId == id select c).ToList();
            }
        }
    }
}
