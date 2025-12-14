using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDotNot8.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser, IEntityTypeConfiguration<ApplicationUser>
{
    public string Address { get; set; }
    
    public required string Token { get; set; }
    
    public List<Bill> Bills { get; set; }
    
    public bool Active { get; set; } = false;
    
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasIndex( e => new {e.Address, e.Active}).IsUnique();
        builder.ToTable("ApplicationUsers");
    }
}