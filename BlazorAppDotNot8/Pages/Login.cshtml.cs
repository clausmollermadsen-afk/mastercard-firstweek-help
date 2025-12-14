using System.Security.Claims;
using BlazorAppDotNot8.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace BlazorAppDotNot8;

public class Login : PageModel
{

    public Login(AuthenticationStateProvider authStateProvider)
    {
        _authProvider = (CustomAuthStateProvider)authStateProvider;
    }
    
    private readonly CustomAuthStateProvider _authProvider;

    [BindProperty(SupportsGet = true)]
    public Guid Token { get; set; }

    public async Task<IActionResult> OnGet(string returnUrl = "/")
    {
        if (await _authProvider.SignIn(Token.ToString()))
        {
            // Tell Blazor to refresh its AuthenticationState
            _authProvider.NotifyUserChanged(); 
            
            LocalRedirect("/PostLogin");
        }

        return Redirect("/NoAccess");
    }
}