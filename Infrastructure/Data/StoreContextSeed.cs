using System;
using System.Text.Json;
using Core.Entities;


namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
{
    // لو مخزن Skinet فاضي (مفيش Products)
    if (!context.Products.Any())
    {
        // نقرأ أول شحنة منتجات من ملف JSON
        var productsData =
            await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

        // نحول النص لقائمة Products
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);


        if (products == null) return;

        context.Products.AddRange(products);

        await context.SaveChangesAsync();

    
    }
}

}
