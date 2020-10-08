namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNMM")]
    public partial class PCNMM
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MSEQ { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TYPE { get; set; }

        [StringLength(80)]
        public string FILENAME { get; set; }

        [StringLength(255)]
        public string FILEPATHX { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WPTYPE { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal TTKEY { get; set; }

        [StringLength(80)]
        public string DOCDESC { get; set; }

        [StringLength(40)]
        public string FILEDESCX { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VISUALAID { get; set; }
    }
}
