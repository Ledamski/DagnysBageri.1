using System.Text.Json;
using MormorDagnysBageri.api.Entities;
namespace MormorDagnysBageri.api.Data;

    public static class Seed
    {
        public static async Task LoadProducts(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        if (context.Products.Any()) return;

        var json = File.ReadAllText("Data/json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
        

    }
    
    public static async Task LoadSuppliers(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        if (context.Suppliers.Any()) return;

        var json = File.ReadAllText("Data/json/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        if (suppliers is not null && suppliers.Count > 0)
        {
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }

    }
    public static async Task LoadSupplierItems(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        if (context.SupplierItems.Any()) return;

        var json = File.ReadAllText("Data/json/supplieritems.json");
        var supplierItems = JsonSerializer.Deserialize<List<SupplierItem>>(json, options);

        if (supplierItems is not null && supplierItems.Count > 0)
        {
            await context.SupplierItems.AddRangeAsync(supplierItems);
            await context.SaveChangesAsync();
        }

    }
        
    }
