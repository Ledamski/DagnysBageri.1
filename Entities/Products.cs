namespace MormorDagnysBageri.api.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ItemNumber { get; set; }
    public int Quantity { get; set; }
    public double PricePerKg { get; set; }
    public IList<SupplierItem> SupplierItems { get; set; }
}