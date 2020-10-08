using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNRFCCOMMENT
    {
        public override string ToString()
        {
            return $"CATEGORYID:>{this.CATEGORYID}< COMMENTID:>{this.COMMENTID}< COMMENTDESC:>{this.COMMENTDESC}< ";
        }
    }
}
