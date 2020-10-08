using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNWITEXT
    {
        public override string ToString()
        {
            return $"PCNNO:>{this.PCNNO}< TTKEY:>{this.TTKEY}< WP:>{this.WP}<";
        }
    }
}
