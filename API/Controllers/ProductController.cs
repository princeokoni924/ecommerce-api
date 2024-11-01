using System;
using Core.Contract;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductRepository _repo):ControllerBase
{

//get all product
 [HttpGet]
 // filtering product
 public async Task <ActionResult<IReadOnlyList<Product>>> GetAllProduct(string? productBrand, string? productType, string? sorting)
 {
 return Ok(await _repo.GetAllProductAsync(productBrand, productType, sorting));
 } 

 //get product by id
 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProductById(int id)
 {
       var productById = await _repo.GetSingleProductByIdAsync(id); 
       if(productById==null)
       {
         return NotFound("Oops,we couldn't find any object on this id to delete");
       } 
       return productById;   
       
 }

 [HttpGet("GetBrands")]
 public async Task<ActionResult<IReadOnlyList<string>>>GetProductByBrands()
 {
    return Ok(await _repo.GetProductByBrandsAsync());
 }

 [HttpGet("GetType")]
 public async Task<ActionResult<IReadOnlyList<string>>> GetProductByType()
 {
   return Ok(await _repo.GetProductByTypesAsync());
 }

 // create product
 [HttpPost("CreateProduct")]
 public async Task<ActionResult<Product>> CreateProduct(Product product)
 {
   _repo.AddProduct(product);
   

   if(await _repo.SaveChangesAsync())
   {
      return CreatedAtAction("GetProductById", new {id = product.Id},product);
   }else
   {
   return BadRequest("Oops, something went wrong while creating the product");
 }
 } 

 // edit product
 [HttpPut("id:int")]
 public async Task<ActionResult<Product>> EditProduct(int id, Product product)
 {
   if(product.Id !=id || !ProductAlreadyExist(id))
   {
     return BadRequest("Oops.... Sorry, we can't edit this product because it already exist");
   }else
    {
   _repo.EditProduct(product);
    }

    if(await _repo.SaveChangesAsync())
    {
     return NoContent();
    }else{
      return BadRequest("Oops,there's a problem updating the product");
    }
 }

[HttpDelete("id:int")]
public async Task<ActionResult> DeleteProducts(int id)
{
 var deleteProduct = await _repo.GetSingleProductByIdAsync(id);
 if(deleteProduct==null)
 {
 return NotFound("Oops, object notfound");
 }
 _repo.DeleteProduct(deleteProduct);
 if(await _repo.SaveChangesAsync()){
  return NotFound();
 }else
 {
  return BadRequest("Oops, sorry we can't delete this product, maybe something went wrong");
 }
}
 // check if the product already exist in database
 private bool ProductAlreadyExist(int id)
 {
  return _repo.ProductExist(id);
 }
}
