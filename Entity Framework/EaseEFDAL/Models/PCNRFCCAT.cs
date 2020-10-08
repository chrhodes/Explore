namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNRFCCAT")]
    public partial class PCNRFCCAT
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal GROUPID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal CATEGORYID { get; set; }

        [Key]
        [Column("SEQ ", Order = 2, TypeName = "numeric")]
        public decimal SEQ_ { get; set; }

        [StringLength(80)]
        public string CATEGORYNAME { get; set; }
    }
}
