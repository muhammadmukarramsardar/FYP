using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApplication.Entities
{
    public class GuardianTime
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public Guardian Guardian { get; set; }
        public int? GuardianId { get; set; }


    }
}
