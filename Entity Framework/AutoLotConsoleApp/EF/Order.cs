namespace AutoLotConsoleApp.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Order
    {
        public int OrderId { get; set; }

        public int CustId { get; set; }

        public int CarId { get; set; }

        //[Column(TypeName = "timestamp")]
        //[MaxLength(8)]
        //[Timestamp]
        //public byte[] Timestamp { get; set; }

        // Navigation Property
        public virtual Customer Customer { get; set; }

        // Navigation Property
        public virtual Car Car { get; set; }
    }
}
