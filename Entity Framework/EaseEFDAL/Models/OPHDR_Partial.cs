using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class OPHDR
    {
        public override string ToString()
        {
            return $"ID:>{this.ID}< RECTYPE:>{this.RECTYPE}< SEQ:>{this.SEQ}< OPNO:>{this.OPNO}< OPDESC:>{this.OPDESC}<";
        }
    }
}
