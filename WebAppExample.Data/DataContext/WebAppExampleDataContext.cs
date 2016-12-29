using System.Data.Entity;
using WebAppExample.Data.Mappings;
using WebAppExample.Lib.Data.Models;

namespace WebAppExample.Data.DataContext
{
    public class WebAppExampleDataContext : DbContext
    {
        public WebAppExampleDataContext() : base("name=WebAppExampleConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
