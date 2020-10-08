using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNNOTE
    {
        public override string ToString()
        {
            return $"PCNNO:>{this.PCNNO}< ENTRYDATE:>{this.ENTRYDATE}< ENG:>{this.ENG}< ";
        }
    }
}
