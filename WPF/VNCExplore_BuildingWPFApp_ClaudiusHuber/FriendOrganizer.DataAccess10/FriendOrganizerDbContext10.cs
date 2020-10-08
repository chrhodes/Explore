using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using FriendOrganizer.Domain;

namespace FriendOrganizer.FriendOrganizer.DataAccess10
{
    public class FriendOrganizerDbContext10 : DbContext
    {
        public FriendOrganizerDbContext10() : base("VNCFriendOrganizerDB")
        {

        }

        public DbSet<Friend05> Friends05 { get; set; }
        //public DbSet<IFriend> Friends05 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // TODO(crhodes)
            // Explore other Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Can use Fluent UI to specify constraints
            // or use DataAnnotations in model class
            // Using DataAnnotations in model class allows them to be used in UI validation

            //modelBuilder.Entity<Friend05>()
            //    .Property(f => f.FirstName)
            //    .IsRequired()
            //    .HasMaxLength(50);

            // Can also do in a separate class

            //modelBuilder.Configurations.Add(new Friend05Configuration());

            // This (may) let us use the DbSet<IFriend> instead of DbSet<Friend05>
            // Nope, didn't work.

            //modelBuilder.Entity<IFriend>().Map(m =>
            //{
            //    //m.MapInheritedProperties();
            //    m.ToTable("Friend05");
            //});

        }
    }

    //public class Friend05Configuration : EntityTypeConfiguration<Friend05>
    //{
    //    public Friend05Configuration()
    //    {
    //        Property(f => f.FirstName)
    //            .IsRequired()
    //            .HasMaxLength(50);
    //    }
    //}
}
