using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDotNot8.Data;

public class Bill: EntityBase<Bill>
{
    public double Amount { get; set; }
    
    public DateTime FromDate { get; set; }
    
    public DateTime ToDate{ get; set; }
    
    public string FK_UserId { get; set; }
    
    public string Status {get; set;}
    
    public List<Payment> Payments { get; set; }
    
    public required ApplicationUser ApplicationUser { get; set; }

    public void Configure(EntityTypeBuilder<Bill> builder)
    {       
        base.Configure(builder);
        
        builder.HasOne(bill => bill.ApplicationUser)
            .WithMany(c => c.Bills)
            .HasForeignKey(bill => bill.FK_UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}