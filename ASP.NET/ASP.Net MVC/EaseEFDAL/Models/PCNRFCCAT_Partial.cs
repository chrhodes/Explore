using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNRFCCAT
    {
        public override string ToString()
        {
            return $"CATEGORYID:>{this.CATEGORYID}< CATEGORYNAME:>{this.CATEGORYNAME}< GROUPID:>{this.GROUPID}< SEQ_:>{this.SEQ_}<";
        }
    }
}
