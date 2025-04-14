namespace MormorDagnysBageri.api.Entities;

public class Supplier
{
    public int SupplierId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public IList<SupplierItem> SupplierItems { get; set; }
    public Product Product { get; internal set;}
}