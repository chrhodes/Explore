using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PCNTASKLIST
    {
        public override string ToString()
        {
            return $"TaskNum:>{this.TaskNum}< TaskDesc:>{this.TaskDesc}<";
        }
    }
}
