using Microsoft.AspNetCore.Routing.Patterns;
using MormorDagnysBageri.api.Entities;

namespace MormorDagnysBageri.api.ViewModels;

public class ProductOrderViewModel
{
    public Product Product { get; set; }
    public IList<SupplierItemViewModel> Products { get; set; }
    public IEnumerable<object> SupplierItemViews { get; internal set; }
}