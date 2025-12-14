using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDotNot8.Data;

public class Payment: EntityBase<Payment>
{
    private string Account { get; set; }
    
    private string Bank { get; set; }
    
    private string Status { get; set; }
    
    private string Description { get; set; }

    public Bill Bill { get; set; }
    
    public long FK_BillId { get; set; }
    
    public void Configure(EntityTypeBuilder<Payment> builder)
    {       
        base.Configure(builder);
        builder.HasOne(payment => payment.Bill)
            .WithMany(c => c.Payments)
            .HasForeignKey(bill => bill.FK_BillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}