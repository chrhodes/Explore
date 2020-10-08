using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNSDAUTHCNT
    {
        public override string ToString()
        {
            return $"AUTHSTATUS:>{this.AUTHSTATUS}< AWAITINGCHECKIN:>{this.AWAITINGCHECKIN}< DocID:>{this.DocID}< LocationDoc:>{this.LocationDoc}<";
        }
    }
}
