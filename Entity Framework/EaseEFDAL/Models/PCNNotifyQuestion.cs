namespace EaseEFDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PCNNotifyQuestion
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal QID { get; set; }

        [StringLength(150)]
        public string NOTIFICATIONQTN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PLANTID { get; set; }
    }
}
