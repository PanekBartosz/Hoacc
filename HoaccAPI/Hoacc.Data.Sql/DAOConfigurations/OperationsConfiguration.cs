using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaccDataSql.DAOConfigurations
{
    public class OperationsConfiguration: IEntityTypeConfiguration<DAO.Operations>
    {
        public void Configure(EntityTypeBuilder<DAO.Operations> builder)
        {
            builder.Property(c => c.Type).IsRequired(); 
            builder.Property(c => c.Date).IsRequired(); 
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.Category).IsRequired(); 
            builder.Property(c => c.Amount).IsRequired(); 
            builder.Property(c => c.UserId).IsRequired();
            
            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Operations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.UserId);

            builder.ToTable("Operations");
        }
    }

}