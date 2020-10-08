namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNTASKEscalate")]
    public partial class PCNTASKEscalate
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GROUPID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal MSEQ { get; set; }

        [StringLength(255)]
        public string TASKDESCX { get; set; }

        [StringLength(30)]
        public string ManagerID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DUEDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EscalateDATE { get; set; }

        [StringLength(255)]
        public string REASONX { get; set; }
    }
}
