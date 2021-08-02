using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalModel
{

    class Factor
    {
        public double RR {get; set;}
        public string Name { get; set; }
        public bool IsAgeSpecific { get; set; }

        public Factor(double rr, string name, bool isagespecific)
        {
            this.RR = rr;
            this.Name = name;
            this.IsAgeSpecific = isagespecific;
        }


    }



}
