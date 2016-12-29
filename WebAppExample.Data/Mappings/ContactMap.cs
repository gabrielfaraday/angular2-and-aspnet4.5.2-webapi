using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebAppExample.Lib.Data.Models;

namespace WebAppExample.Data.Mappings
{
    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            HasKey(x => x.Id)
                .Map(x => x.ToTable("Contact"))
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.Email).HasMaxLength(50).IsRequired();
            Property(x => x.BirthDate);
            Property(x => x.Address).HasMaxLength(100);
            Property(x => x.Phone).HasMaxLength(11);


        }
    }
}
