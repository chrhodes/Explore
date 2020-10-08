namespace EaseEFDAL.Models

{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OPHDR")]
    public partial class OPHDR
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
        public decimal OPSEQ { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(6)]
        public string OPNO { get; set; }

        [StringLength(60)]
        public string OPDESC { get; set; }

        [StringLength(15)]
        public string WORKCENT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MAXBATCH { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DATAREC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PFDCYC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PFDSETUP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REALCYC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REALSETUP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ACOSTRATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ACYCTIME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ASETUPTIME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MATRECNO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MACHRECNO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EffectiveFrom { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ISMETRIC { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MISCFLAG1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PCNNO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MISCFLAG2 { get; set; }

        [StringLength(20)]
        public string USERDEF1 { get; set; }

        [StringLength(20)]
        public string USERDEF2 { get; set; }

        [StringLength(20)]
        public string USERDEF3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TOOLCOSTRUN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TOOLCOSTSU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NSL { get; set; }

        [StringLength(1)]
        public string PROCESSFLAG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NOMEN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BALANCEFLAG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ALTFLAG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? COSTKEY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RULEID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? STATIONTIME { get; set; }

        [StringLength(30)]
        public string Engineer { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ReleasedFlag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InUseFlag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CRITOPTIME { get; set; }

        [StringLength(5)]
        public string CRITOP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? VA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NVA { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ESSNVA { get; set; }

        [StringLength(3)]
        public string OpRev { get; set; }

        [StringLength(30)]
        public string CheckOut_Engineer { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OpRelDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EffectiveTo { get; set; }

        [StringLength(20)]
        public string SPARE1 { get; set; }

        [StringLength(20)]
        public string SPARE2 { get; set; }

        [StringLength(20)]
        public string SPARE3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SHAREDOPID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BasicRunTime { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BasicSetupTime { get; set; }

        [StringLength(30)]
        public string KVICODE { get; set; }
    }
}
