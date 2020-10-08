namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNSD")]
    public partial class PCNSD
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal PCNKey { get; set; }

        [Column(TypeName = "numeric")]
        public decimal DocID { get; set; }

        [StringLength(80)]
        public string DocDesc { get; set; }

        [StringLength(3)]
        public string RevNumber { get; set; }

        [Column(TypeName = "numeric")]
        public decimal DocSeq { get; set; }

        [StringLength(1)]
        public string ApprovedStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LocationDoc { get; set; }

        [StringLength(30)]
        public string KeyFieldValue { get; set; }
    }
}
