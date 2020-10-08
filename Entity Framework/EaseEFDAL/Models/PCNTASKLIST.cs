namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNTASKLIST")]
    public partial class PCNTASKLIST
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal PLANTID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal TEMPLATEID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GROUPID { get; set; }

        [StringLength(30)]
        public string USERID { get; set; }

        [StringLength(25)]
        public string UserRole { get; set; }

        [StringLength(30)]
        public string Manager { get; set; }

        [StringLength(255)]
        public string TaskDesc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Critical { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaskNum { get; set; }
    }
}
