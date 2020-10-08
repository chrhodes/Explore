using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNNotifyQuestion
    {
        public override string ToString()
        {
            return $"QID:>{this.QID}< NOTIFICATIONQTN:>{this.NOTIFICATIONQTN}< PLANTID:>{this.PLANTID}< ";
        }
    }
}
