namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNWITHDRAW")]
    public partial class PCNWITHDRAW
    {
        [Key]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [StringLength(1)]
        public string WITHDRAWTYPE { get; set; }

        [StringLength(255)]
        public string REASON { get; set; }
    }
}
