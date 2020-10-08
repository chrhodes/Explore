using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNSD
    {
        public override string ToString()
        {
            return $"PCNNO:>{this.PCNNO}< PCNKey:>{this.PCNKey}< DocID:>{this.DocID}< DocDesc:>{this.DocDesc}< LocationDoc:>{this.LocationDoc}<";
        }
    }
}
