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
        
    }
}
