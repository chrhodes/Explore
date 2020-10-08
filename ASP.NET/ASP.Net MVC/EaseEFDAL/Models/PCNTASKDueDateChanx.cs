namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PCNTASKDueDateChanx
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal MSEQ { get; set; }

        [StringLength(255)]
        public string TaskDESCX { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TaskSEQ { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DATEX { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OldDATEX { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NewDATEX { get; set; }

        [StringLength(255)]
        public string CommentX { get; set; }

        [StringLength(30)]
        public string ChangedBy { get; set; }

        [StringLength(30)]
        public string Spare1 { get; set; }

        [StringLength(30)]
        public string Spare2 { get; set; }

        [StringLength(30)]
        public string Spare3 { get; set; }
    }
}
