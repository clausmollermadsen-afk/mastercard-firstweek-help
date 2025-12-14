using BlazorAppDotNot8.Data;
using BlazorAppDotNot8.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAppDotNot8.Components.Pages;

public partial class AddPayment : ComponentBase
{
    [Inject] private BillService BillService  { get; set; }
 
    private Bill? Bill { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Bill = await BillService.GetBill(BillId); 
    }
}