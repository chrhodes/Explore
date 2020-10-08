using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseEFDAL.Models
{
    public partial class PARTXREF
    {
        public override string ToString()
        {
            return $"ID:>{this.ID}< PlantID:>{this.PlantID}< PARTNO:>{this.PARTNO}<";
        }
    }
}
