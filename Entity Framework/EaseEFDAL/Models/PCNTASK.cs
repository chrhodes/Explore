namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNTASKS")]
    public partial class PCNTASK
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal TEMPLATEID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GROUPID { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal MSEQ { get; set; }

        [StringLength(255)]
        public string DESCX { get; set; }

        [StringLength(15)]
        public string APPROVERID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DUEDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal APPLICABLE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal CRITICAL { get; set; }

        [StringLength(1)]
        public string TASKSTATUS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SignoffDate { get; set; }

        [StringLength(25)]
        public string UserRole { get; set; }

        [StringLength(30)]
        public string Manager { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EscalateDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaskSeq { get; set; }

        [StringLength(250)]
        public string COMMENTX { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SEVERITY { get; set; }
    }
}
