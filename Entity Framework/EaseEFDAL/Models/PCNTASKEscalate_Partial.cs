using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNTASKEscalate
    {
        public override string ToString()
        {
            return $"PCNNO:>{this.PCNNO}< MSEQ:>{this.MSEQ}< TASKDESCX:>{this.TASKDESCX}<";
        }
    }
}
