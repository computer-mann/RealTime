using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UuidExtensions;

namespace RealTime.Models.EntityConfigurations
{
    public class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(op => op.Id).HasColumnType("char(25)");
            builder.ToTable("AppUsers");
        }
    }
}
