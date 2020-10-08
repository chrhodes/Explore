namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNRFCGROUP")]
    public partial class PCNRFCGROUP
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal GROUPID { get; set; }

        [Column("SEQ ", TypeName = "numeric")]
        public decimal? SEQ_ { get; set; }

        [StringLength(80)]
        public string GROUPNAME { get; set; }
    }
}
