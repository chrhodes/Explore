namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ROUTEHDR")]
    public partial class ROUTEHDR
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string RECTYPE { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal SEQ { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LOTSIZE { get; set; }

        [StringLength(10)]
        public string AUTHREL { get; set; }

        [StringLength(30)]
        public string ENG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RELDATE { get; set; }

        [StringLength(20)]
        public string USER0 { get; set; }

        [StringLength(20)]
        public string USER1 { get; set; }

        [StringLength(20)]
        public string USER2 { get; set; }

        [StringLength(30)]
        public string USER3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MATCOST { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EXPUNIT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EXPLOT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CCOST { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SCOST { get; set; }

        [StringLength(1)]
        public string CHANGE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TCOST { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TOOLCOSTS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NOP { get; set; }

        [StringLength(50)]
        public string PARTDESC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NET { get; set; }

        [StringLength(30)]
        public string USERID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SETUPHRS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RUNHRS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FLAG1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FLAG2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FLAG3 { get; set; }

        [StringLength(30)]
        public string MASTERPART { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TAKTTIME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NetDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PLANTID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PARTREV { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LOCKPLAN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ContinousFlow { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SHLevel { get; set; }
    }
}
