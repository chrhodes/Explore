using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNAUTHGROUP
    {
        public override string ToString()
        {
            return $"AUTHFLAG:>{this.AUTHFLAG}< GROUPID:>{this.GROUPID}< NOTIFYFLAG:>{this.NOTIFYFLAG}< QID:>{this.QID}<";
        }
    }
}
