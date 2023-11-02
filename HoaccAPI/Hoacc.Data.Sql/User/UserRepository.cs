using System.Threading.Tasks;
using HoaccIData.User;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.User
{
    public class UserRepository: IUserRepository
    {
        private readonly HoaccDbContext _context;
        private IUserRepository _userRepositoryImplementation;

        public UserRepository(HoaccDbContext context)
        {
            _context = context;
        }

        public async Task<HoaccDomain.User.User> GetUser(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(x=>x.Email == email);
            return new HoaccDomain.User.User(user.UserId,
                user.Email,
                user.Password);
        }
        
        public async Task<HoaccDomain.User.User> GetUser(int userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(x=>x.UserId == userId);
            return new HoaccDomain.User.User(user.UserId,
                user.Email,
                user.Password);
        }
        
        public async Task<int> CreateUser(HoaccDomain.User.User user)
        {
            var userDAO =  new DAO.User { 
                UserId = user.UserId,
                Email = user.Email,
                Password = user.Password
            };
            await _context.AddAsync(userDAO);
            await _context.SaveChangesAsync();
            return userDAO.UserId;
        }

        public async Task UpdateUserPassword(int userId, HoaccDomain.User.User user)
        {
            var updateUserPassword = await _context.User.FirstOrDefaultAsync(x=>x.UserId == user.UserId);
            updateUserPassword.Password = user.Password;
            await _context.SaveChangesAsync();
        }

    }

}