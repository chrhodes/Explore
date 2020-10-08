namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNSDAUTHCNT")]
    public partial class PCNSDAUTHCNT
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal DocID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal DocSeq { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NUMSIGN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DONESIGN { get; set; }

        [StringLength(1)]
        public string AUTHSTATUS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AWAITINGCHECKIN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PENDINGDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LocationDoc { get; set; }
    }
}
