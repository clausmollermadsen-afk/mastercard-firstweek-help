using System.Security.Claims;
using BlazorAppDotNot8.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDotNot8.Services;

public class CurrentUser
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Task<ApplicationUser?> GetUser()
    {
        var user = _httpContextAccessor.HttpContext!.User;
        var id = user.FindFirst(ClaimTypes.Sid)?.Value;

        if (id == null)
            return null;
        
        return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}