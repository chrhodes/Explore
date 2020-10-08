namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PCNRFCCATEGORY")]
    public partial class PCNRFCCATEGORY
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal CATEGORYID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal SEQ { get; set; }

        [StringLength(80)]
        public string CATEGORYDESC { get; set; }
    }
}
