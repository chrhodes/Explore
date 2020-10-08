using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNSDAuth
    {
        public override string ToString()
        {
            return $"AUTHDATE:>{this.AUTHDATE}< AUTHORIZER:>{this.AUTHORIZER}< AUTHSTATUS:>{this.AUTHSTATUS}< COMMENTX:>{this.COMMENTX}<";
        }
    }
}
