using System.Data.Entity.ModelConfiguration;
using WebAppExample.Lib.Data.Models;

namespace WebAppExample.Data.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(x => x.UserName)
               .Map(x => x.ToTable("UserLogin"))
               .Property(x => x.UserName)
               .HasMaxLength(20);

            Property(x => x.PasswordHash).HasMaxLength(250).HasColumnName("Password");
        }
    }
}
