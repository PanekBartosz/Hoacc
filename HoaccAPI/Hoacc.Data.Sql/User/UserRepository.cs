using HoaccCommon.User;
using Microsoft.EntityFrameworkCore;

namespace HoaccDataSql.User;

public class UserRepository : IUserRepositoryDTO
{
    private readonly HoaccDbContext _context;
    private IUserRepositoryDTO _userRepositoryImplementation;

    public UserRepository(HoaccDbContext context)
    {
        _context = context;
    }

    public async Task<UserDTO> GetUser(string email)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        return new UserDTO(user.UserId,
            user.Email,
            user.Password);
    }

    public async Task<UserDTO> GetUser(int userId)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == userId);
        return new UserDTO(user.UserId,
            user.Email,
            user.Password);
    }
    
    public async Task<string?> GetUserEmail(int userId)
    {
        var userEmail = await _context.User
            .Where(x => x.UserId == userId)
            .Select(x => x.Email)
            .FirstOrDefaultAsync();

        return userEmail;
    }

    public async Task<int> CreateUser(UserDTO user)
    {
        if (await _context.User.AnyAsync(u => u.Email == user.Email))
        {
            throw new Exception("Email address is already in use.");
        }
        var userDAO = new DAO.User
        {
            UserId = user.UserId,
            Email = user.Email,
            Password = user.Password
        };
        await _context.AddAsync(userDAO);
        await _context.SaveChangesAsync();
        return userDAO.UserId;
    }

    public async Task UpdateUserPassword(int userId, UserDTO user)
    {
        var updateUserPassword = await _context.User.FirstOrDefaultAsync(x => x.UserId == user.UserId);
        updateUserPassword.Password = user.Password;
        await _context.SaveChangesAsync();
    }
}