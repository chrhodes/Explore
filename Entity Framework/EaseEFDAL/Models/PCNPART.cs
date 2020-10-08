namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNPARTS")]
    public partial class PCNPART
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal PCNKEY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ID { get; set; }

        [Required]
        [StringLength(1)]
        public string RECTYPE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal SEQ { get; set; }

        [Required]
        [StringLength(40)]
        public string PARTNO { get; set; }

        [Required]
        [StringLength(6)]
        public string OPNO { get; set; }

        [StringLength(3)]
        public string OPREV { get; set; }

        [StringLength(15)]
        public string CELLNO { get; set; }

        [StringLength(1)]
        public string APPROVEDSTATUS { get; set; }

        [StringLength(50)]
        public string PARTDESC { get; set; }

        [StringLength(60)]
        public string DBNAME { get; set; }

        [StringLength(25)]
        public string PLANTYPE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SHAREDPARTID { get; set; }

        [StringLength(40)]
        public string SHAREDPARTNO { get; set; }
    }
}
