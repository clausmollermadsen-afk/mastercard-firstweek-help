using System.Security.Claims;
using BlazorAppDotNot8.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAppDotNot8.Auth;

public class CustomAuthStateProvider: AuthenticationStateProvider
{
    public CustomAuthStateProvider(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _contex = context;
    }
    
    private ClaimsPrincipal _user = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly ApplicationDbContext _contex;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {         
        var user = _httpContextAccessor.HttpContext?.User 
                         ?? _user;
        return Task.FromResult(new AuthenticationState(user));
    }
    
    public void NotifyUserChanged()
    {
        var authState = Task.FromResult(new AuthenticationState(_user));
        NotifyAuthenticationStateChanged(authState);
    }

    
    public async Task<bool> SignIn(string token)
    {
        var user = _contex.Users.SingleOrDefault(u => u.Token == token);
        if (user == null)
            return false;
        
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Address),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Sid, user.Id),
            }, "Cookies");

        _user = new ClaimsPrincipal(identity);
        var principal = new ClaimsPrincipal(identity);

        await _httpContextAccessor.HttpContext!.SignInAsync("Cookies", principal);
        NotifyUserChanged();
        return true;
    }

    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync("Cookies");

        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}