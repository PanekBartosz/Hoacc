using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaccDataSql.DAOConfigurations
{
    public class NotificationConfiguration: IEntityTypeConfiguration<DAO.Notification>
    {
        public void Configure(EntityTypeBuilder<DAO.Notification> builder)
        {
            builder.Property(c => c.Name).IsRequired(); 
            builder.Property(c => c.Date).IsRequired(); 
            builder.Property(c => c.Amount).IsRequired(); 
            builder.Property(c => c.UserId).IsRequired();
            
            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Notification)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.UserId);

            builder.ToTable("Notification");
        }
    }

}