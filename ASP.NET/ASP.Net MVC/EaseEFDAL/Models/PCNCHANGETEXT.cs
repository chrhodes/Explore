namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNCHANGETEXT")]
    public partial class PCNCHANGETEXT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal CHANGETYPE { get; set; }

        [StringLength(255)]
        public string CHANGETEXT { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal CHANGETEXTSEQ { get; set; }
    }
}
