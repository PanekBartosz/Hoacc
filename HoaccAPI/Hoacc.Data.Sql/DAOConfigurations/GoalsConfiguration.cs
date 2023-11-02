using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HoaccDataSql.DAOConfigurations
{
    public class GoalsConfiguration: IEntityTypeConfiguration<DAO.Goals>
    {
        public void Configure(EntityTypeBuilder<DAO.Goals> builder)
        {
            builder.Property(c => c.Name).IsRequired(); 
            builder.Property(c => c.CurrentAmount).IsRequired(); 
            builder.Property(c => c.GoalAmount).IsRequired(); 
            builder.Property(c => c.UserId).IsRequired();
            
            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Goals)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.UserId);

            builder.ToTable("Goals");
        }
    }

}