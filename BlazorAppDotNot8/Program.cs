using BlazorAppDotNot8.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorAppDotNot8.Components;
using BlazorAppDotNot8.Data;
using BlazorAppDotNot8.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton(TimeProvider.System);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";       // redirect here if not logged in
        options.AccessDeniedPath = "/NoAccess"; // redirect if forbidden
        options.ReturnUrlParameter = "returnUrl";
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

var connectionString =
    @"DataSource= C:\Users\claus\RiderProjects\mastercard-firstweek-help\BlazorAppDotNot8\App.db;Cache=Shared";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BillService>();
builder.Services.AddScoped<CurrentUser>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages(); 
var app = builder.Build();

app.UseAuthentication();   // must come before UseAuthorization
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.MapRazorPages();                // <-- add this



app.Run();
