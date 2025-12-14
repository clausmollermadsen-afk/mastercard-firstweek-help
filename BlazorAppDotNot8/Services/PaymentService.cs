using BlazorAppDotNot8.Data;
using SQLitePCL;

namespace BlazorAppDotNot8.Services;

public class PaymentService
{
    private readonly ApplicationDbContext _context;

    public PaymentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddPayment(Bill bill)
    {
        
    }
}