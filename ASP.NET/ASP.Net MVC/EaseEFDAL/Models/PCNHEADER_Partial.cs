using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNHEADER
    {
        public override string ToString()
        {
            return $"PCNNO:>{this.PCNNO}< PCNTYPE:>{this.PCNTYPE}< PCNCATEGORY:>{this.PCNCATEGORY}<";
        }
    }
}
