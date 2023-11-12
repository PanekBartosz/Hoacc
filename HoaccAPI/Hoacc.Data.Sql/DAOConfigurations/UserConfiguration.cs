using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaccDataSql.DAOConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<DAO.User>
{
    public void Configure(EntityTypeBuilder<DAO.User> builder)
    {
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.Password).IsRequired();

        builder.ToTable("User");
    }
}