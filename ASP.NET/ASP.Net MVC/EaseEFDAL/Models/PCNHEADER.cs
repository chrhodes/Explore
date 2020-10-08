namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNHEADER")]
    public partial class PCNHEADER
    {
        [Key]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [StringLength(30)]
        public string ECNNO { get; set; }

        [StringLength(30)]
        public string PDNNO { get; set; }

        [StringLength(30)]
        public string ENGINEER { get; set; }

        [StringLength(1)]
        public string PCNTYPE { get; set; }

        [StringLength(45)]
        public string PLANT { get; set; }

        [StringLength(40)]
        public string WORKGROUP { get; set; }

        [StringLength(25)]
        public string PCNCATEGORY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REQUESTDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? IMPLEMENTDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? EXPIRATIONDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SIGNOFFDATE { get; set; }

        [StringLength(1)]
        public string PCNSTATUS { get; set; }

        [StringLength(255)]
        public string COMMENTX { get; set; }

        [StringLength(255)]
        public string REASON { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NET { get; set; }

        [StringLength(30)]
        public string USERID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NETDATE { get; set; }

        [StringLength(25)]
        public string MODEL { get; set; }

        [StringLength(20)]
        public string ALTPCNNO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PLANTID { get; set; }

        [StringLength(1)]
        public string ManagerOverRide { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RFCCreatedDate { get; set; }

        [StringLength(30)]
        public string ProcDocWriter { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CreatedDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ActualImplementDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PRIORITY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FastTrack { get; set; }

        [StringLength(25)]
        public string FTCategory { get; set; }
    }
}
