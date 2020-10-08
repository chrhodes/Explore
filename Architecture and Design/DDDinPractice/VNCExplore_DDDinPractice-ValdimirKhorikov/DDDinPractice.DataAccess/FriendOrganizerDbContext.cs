using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using FriendOrganizer.Domain;

namespace FriendOrganizer.DataAccess
{
    public class FriendOrganizerDbContext : DbContext
    {
        public FriendOrganizerDbContext() : base("VNCFriendOrganizerDB")
        {
        }

        public DbSet<Friend> Friends{ get; set; }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public DbSet<FriendPhoneNumber> FriendPhoneNumbers { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

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
