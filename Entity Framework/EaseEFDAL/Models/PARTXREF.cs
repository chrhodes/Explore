namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PARTXREF")]
    public partial class PARTXREF
    {
        [Column(TypeName = "numeric")]
        public decimal ID { get; set; }

        [StringLength(40)]
        public string PARTNO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PlantID { get; set; }
    }
}
