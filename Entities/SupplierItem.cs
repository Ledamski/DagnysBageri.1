namespace MormorDagnysBageri.api.Entities;

public class SupplierItem
{
    public int SupplierId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ItemNumber { get; set; }
    public int Quantity { get; set; }
    public double PricePerKg { get; set; }
    
    public Supplier Supplier { get; set; }
    public Product Product { get; set; }


}