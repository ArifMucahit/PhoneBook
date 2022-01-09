using ContactService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Data
{
    public class PhoneBookContext:DbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options)
        {

        }

        public DbSet<Person> Person { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(x => x.UID);
            });

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.HasKey(x => x.ContactID);
            });
        }
    }   

   

}
