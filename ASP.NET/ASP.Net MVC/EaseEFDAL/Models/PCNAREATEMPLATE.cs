namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNAREATEMPLATE")]
    public partial class PCNAREATEMPLATE
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal PLANTID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal TEMPLATEID { get; set; }

        [Required]
        [StringLength(150)]
        public string TEMPLATENAME { get; set; }
    }
}
