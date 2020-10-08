using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNWITHDRAW
    {
        public override string ToString()
        {
            return $"PCNNO:>{this.PCNNO}< REASON:>{this.REASON}< WITHDRAWTYPE:>{this.WITHDRAWTYPE}<";
        }
    }
}
