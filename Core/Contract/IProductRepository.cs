using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Contract;

public interface IProductRepository
{
// filtering the product and sorting
Task <IReadOnlyList<Product>>GetAllProductAsync(string? productBrand, string? productType, string? sorting);
Task<Product?> GetSingleProductByIdAsync(int id);
Task<IReadOnlyList<string>>GetProductByBrandsAsync();
Task<IReadOnlyList<string>>GetProductByTypesAsync();

void AddProduct(Product product);
void EditProduct(Product product);
void DeleteProduct(Product product);
bool ProductExist(int id);
Task<bool>SaveChangesAsync();
//Task<int>CountAsync(ISpecification<T> specification);
}

