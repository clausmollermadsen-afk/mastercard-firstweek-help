using BlazorAppDotNot8.Data;

namespace BlazorAppDotNot8.Services;

public class BillService
{
    
    private readonly ApplicationDbContext _context;
    private readonly CurrentUser _currentUser;

    public BillService(ApplicationDbContext context, CurrentUser currentUser) 
    {
        _context = context;
        _currentUser =  currentUser;
    }
    
    public async Task AddBills()
    {
        foreach (var user in  _context.Users.Where(x => x.Active))
        {
            _context.Bills.Add(new Bill()
            {
                ApplicationUser = user,
                Amount = 150,
                FromDate = DateTime.Now.Date,
                ToDate = DateTime.Now.AddDays(1),
                Status = "Created",
                FK_UserId = user.Id
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Bill>> GetBills()
    {

        var user = await _currentUser.GetUser();
        if (user == null)
            return null;
        
        return  _context.Bills.Where( x => x.ApplicationUser == user).ToList();
    }

    public async Task<Bill?> GetBill(long billId)
    {
        var user = await _currentUser.GetUser();
        if (user == null)
            return null;
        
        return  _context.Bills.FirstOrDefault( x => x.ApplicationUser == user);
    }
}