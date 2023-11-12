using HoaccCommon.Operations;
using Microsoft.EntityFrameworkCore;
using HoaccDataSql.DAOConfigurations;
using HoaccDataSql.DAO;

namespace HoaccDataSql
{
    public class HoaccDbContext:DbContext
    {
        public HoaccDbContext(DbContextOptions option) : base(option)
        { }
    
        //Set the classes in the DAO folder as database tables
        public virtual DbSet<DAO.User> User { get; set; }
        
        public virtual DbSet<DAO.Goals> Goals { get; set; }
        
        public virtual DbSet<DAO.Notification> Notification { get; set; }
        
        public virtual DbSet<DAO.Operations> Operations { get; set; }
        
        //Changing ENUM mapping from default "int" to "string" for Category column in Operations Tab
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DAO.Operations>()
                .Property(e => e.Category)
                .HasConversion(
                    v => v.ToString(),
                    v => (
                        OperationsDTO.CategoryType)
                        Enum.Parse(typeof(OperationsDTO.CategoryType), v)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
