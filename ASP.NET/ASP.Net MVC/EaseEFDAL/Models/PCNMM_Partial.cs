using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNMM
    {
        public override string ToString()
        {
            return $"PCNNO:>{this.PCNNO}< TTKEY:>{this.TTKEY}< TYPE:>{this.TYPE}< WPTYPE:>{this.WPTYPE}< ";
        }
    }
}
