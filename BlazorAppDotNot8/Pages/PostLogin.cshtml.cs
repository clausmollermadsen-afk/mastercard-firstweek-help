using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorAppDotNot8;

public class PostLogin : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        await HttpContext.Response.CompleteAsync();
        return LocalRedirect("/_Host");
    }
}