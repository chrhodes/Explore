namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNAUTHGROUPS")]
    public partial class PCNAUTHGROUP
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal QID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal GROUPID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NOTIFYFLAG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AUTHFLAG { get; set; }
    }
}
