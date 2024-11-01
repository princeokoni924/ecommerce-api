using System;
using System.Security.Cryptography.X509Certificates;
using Core.Contract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext _db) : IProductRepository
{
 

    

    public void AddProduct(Product product)
    {
        _db.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        _db.Products.Remove(product);
    }

    public void EditProduct(Product product)
    {
        _db.Entry(product).State =EntityState.Modified;
        
    }

    public async Task<IReadOnlyList<Product>> GetAllProductAsync(string? productBrand, string? productType, string? sorting)
    {
        // configuring the query
        var query = _db.Products.AsQueryable();
        if(!string.IsNullOrWhiteSpace(productBrand))
        {
            query = query.Where(brandExp=>brandExp.Brand==productBrand);
        }else if(!string.IsNullOrWhiteSpace(productType))
        {
            query = query.Where(typeExp=>typeExp.ProductType==productType);
        }
        
         if(!string.IsNullOrWhiteSpace(sorting))
        {
          query = sorting switch
          {
            "PriceAscending"=> query.OrderBy(price =>price.Price),
            "PriceDescending"=> query.OrderByDescending(descPrice=>descPrice.Price),
            _=>query.OrderBy(Ascname=>Ascname.Name)
          };
        }
        
       return await query.ToListAsync();
        
    }

    public async Task<IReadOnlyList<string>> GetProductByBrandsAsync()
    {
      return  await _db.Products.Select(getproductsByBrandExp=>getproductsByBrandExp.Brand)
        .Distinct()
        .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetProductByTypesAsync()
    {
        return await _db.Products.Select(getProductsByTypesExp=>getProductsByTypesExp.ProductType)
               .Distinct()
               .ToListAsync();
    }

    public async Task<Product?> GetSingleProductByIdAsync(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    public bool ProductExist(int id)
    {
      return  _db.Products.Any(z=>z.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync()>0;
    }
}

