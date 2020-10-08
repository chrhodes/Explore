namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNRFCCOMMENT")]
    public partial class PCNRFCCOMMENT
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal CATEGORYID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal COMMENTID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal SEQ { get; set; }

        [StringLength(80)]
        public string COMMENTDESC { get; set; }
    }
}
