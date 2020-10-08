using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNRFCGROUP
    {
        public override string ToString()
        {
            return $"GROUPID:>{this.GROUPID}< GROUPNAME:>{this.GROUPNAME}< SEQ_:>{this.SEQ_}< ";
        }
    }
}
