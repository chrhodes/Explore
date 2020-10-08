namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SUBHDR")]
    public partial class SUBHDR
    {
        [Column(TypeName = "numeric")]
        public decimal ID { get; set; }

        [Required]
        [StringLength(1)]
        public string RECTYPE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal SEQ { get; set; }

        [Required]
        [StringLength(6)]
        public string OPNO { get; set; }

        [Key]
        [Column(TypeName = "numeric")]
        public decimal SHID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal SHSEQ { get; set; }

        [StringLength(80)]
        public string DESCX { get; set; }

        [StringLength(8)]
        public string CHANGEFLAG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MODEL1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MODEL2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DESTINATION1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DESTINATION2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OPTION1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OPTION2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EXCLUDEFROMPRINT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SETUPTIME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CYCLETIME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GANTTSTARTFROM { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LBSTICKY { get; set; }

        [StringLength(20)]
        public string MISCFLAG1 { get; set; }

        [StringLength(20)]
        public string MISCFLAG2 { get; set; }

        [StringLength(20)]
        public string MISCFLAG3 { get; set; }

        [StringLength(20)]
        public string MISCFLAG4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SHType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LaborType { get; set; }

        [StringLength(2)]
        public string NumMen { get; set; }

        [StringLength(255)]
        public string DESC2 { get; set; }

        [StringLength(15)]
        public string PLATTENID { get; set; }
    }
}
