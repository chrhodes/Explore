namespace AutoLotConsoleApp.EF
{
    using System.Data.Entity;

    public partial class AutoLotEntities : DbContext
    {
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
        }

        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CreditRisk>()
            //    .Property(e => e.Timestamp)
            //    .IsFixedLength();

            //modelBuilder.Entity<Customer>()
            //    .Property(e => e.Timestamp)
            //    .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Car>()
            //    .Property(e => e.Timestamp)
            //    .IsFixedLength();

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Order>()
            //    .Property(e => e.Timestamp)
            //    .IsFixedLength();
        }
    }
}
