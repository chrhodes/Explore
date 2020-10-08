using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNPART
    {
        public override string ToString()
        {
            return $"ID:>{this.ID}< PCNNO:>{this.PCNNO}< OPNO:>{this.OPNO}< OPREV:>{this.OPREV}<";
        }
    }
}
