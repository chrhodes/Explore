namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNWITEXT")]
    public partial class PCNWITEXT
    {
        [Key]
        [StringLength(10)]
        public string PCNNO { get; set; }

        [Column(TypeName = "image")]
        public byte[] TEXT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTKEY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WP { get; set; }
    }
}
