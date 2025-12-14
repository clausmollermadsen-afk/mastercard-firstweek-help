using BlazorAppDotNot8.Data;

namespace BlazorAppDotNot8.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddTestUsers()
    {
        for (int i = 0; i < 100; i++)
        {
            _context.Users.Add(
                new ApplicationUser()
                {
                    Address = "Address" + i,
                    Email = "Email" + i,
                    Token = Guid.NewGuid().ToString(),
                    UserName = "UserName" + i,
                    Active = true
                }
            );
            
        }
        _context.SaveChanges();
    }
    
    public List<ApplicationUser> GetUsers() => _context.Users.ToList();

    public async Task UpdateUserAsync(ApplicationUser user)
    {
        _context.Users.Update(user);
        _context.SaveChangesAsync();
    }
}