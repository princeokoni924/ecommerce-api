using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController:ControllerBase
{
private readonly StoreContext _db;
 public ProductController(StoreContext db)
 {
   _db = db;
                
 }
//get all product
 [HttpGet]
 public async Task <ActionResult<IEnumerable<Product>>> GetAllProduct()
 {
 return await _db.Products.ToListAsync();
 } 

 //get product by id
 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProductById(int id)
 {
       var productById = await _db.Products.FindAsync(id); 
       if(productById==null)
       {
         return NotFound("Oops,we couldn't find any object on this id to delete");
       } 
       return productById;      
 }

 // create product
 [HttpPost]
 public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
 {
   _db.Products.Add(product);
   await _db.SaveChangesAsync();
   return product;
 }

 // edit product
 [HttpPut("id:int")]
 public async Task<ActionResult<Product>> EditProduct(int id, Product product)
 {
   if(product.Id !=id || !ProductAlreadyExist(id))
   {
     return BadRequest("Oops.... Sorry, we can't edit this product");
   }
   _db.Entry(product).State= EntityState.Modified;
  await _db.SaveChangesAsync();
  return NoContent();
 }

[HttpDelete("id:int")]
public async Task<ActionResult> DeleteProduct(int id)
{
 var deleteProduct = await _db.Products.FindAsync(id);
 if(deleteProduct==null)
 {
 return NotFound("Oops, object notfound");
 }
 _db.Products.Remove(deleteProduct);
 await _db.SaveChangesAsync();
 return NoContent();
}
 // check if the product already exist in database
 private bool ProductAlreadyExist(int id)
 {
  return _db.Products.Any(c=>c.Id ==id);
 }
}
