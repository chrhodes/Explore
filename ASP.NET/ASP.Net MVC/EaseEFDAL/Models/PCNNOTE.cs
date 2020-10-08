namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNNOTES")]
    public partial class PCNNOTE
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal ENTRYSEQ { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ENTRYDATE { get; set; }

        [StringLength(30)]
        public string ENG { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal COMMENTSEQ { get; set; }

        [StringLength(255)]
        public string COMMENTx { get; set; }
    }
}
