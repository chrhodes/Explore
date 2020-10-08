using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNCHANGETEXT
    {
        public override string ToString()
        {
            return $"CHANGETEXT:>{this.CHANGETEXT}< CHANGETEXTSEQ:>{this.CHANGETEXTSEQ}< CHANGETYPE:>{this.CHANGETYPE}< PCNNO:>{this.PCNNO}<";
        }
    }
}
