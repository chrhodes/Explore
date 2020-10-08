using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNAREATEMPLATE
    {
        public override string ToString()
        {
            return $"TEMPLATEID:>{this.TEMPLATEID}< PLANTID:>{this.PLANTID}< TEMPLATENAME:>{this.TEMPLATENAME}<";
        }
    }
}
