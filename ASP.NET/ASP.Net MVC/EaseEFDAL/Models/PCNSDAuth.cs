namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNSDAuth")]
    public partial class PCNSDAuth
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal DocID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal DocSeq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GROUPID { get; set; }

        [StringLength(30)]
        public string AUTHORIZER { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AUTHDATE { get; set; }

        [StringLength(1)]
        public string AUTHSTATUS { get; set; }

        [StringLength(255)]
        public string COMMENTX { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LocationDoc { get; set; }
    }
}
