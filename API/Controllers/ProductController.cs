using API.RequestHelper;
using Core.Contract.IGeneric;
using Core.Contract.SpecificationServices;
using Core.Entities;
using Core.Params;
using Infrastructure.Data.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class ProductController(IGenericRepo<Product> _repo):BaseApiController
{

//get all product
 [HttpGet]
 // Cleaning up controller
 public async Task <ActionResult<IReadOnlyList<Product>>> GetProduct([FromQuery] ProductSpecParams specParams)
 {
    var spec = new ProductSpecification(specParams);
    return await CreatePageResult(_repo, spec, specParams.PageIndex, specParams.PageSize);
 } 

 //get product by id
 [HttpGet("{id:int}")]
 public async Task<ActionResult<Product>> GetProductById(int id)
 {
       var productById = await _repo.GetDataByIdAsync(id); 
       if(productById==null)
       {
         return NotFound("Oops,we couldn't find any object on this id to delete");
       } 
       return productById;   
       
 }

 [HttpGet("getBrands")]
 public async Task<ActionResult<IReadOnlyList<string>>>GetProductByBrands()
 {

  //Implementating ToDo here
  var brandSpec = new BrandListSpecifications();

    return Ok( await _repo.ListSpecProjAsync(brandSpec));
 }

 [HttpGet("getType")]
 public async Task<ActionResult<IReadOnlyList<string>>> GetProductByType()
 {
  //Implementating ToDo here
  var typeSpec = new TypeListSpecifications();
   return Ok(await _repo.ListSpecProjAsync(typeSpec));
 }

 // create product
 [HttpPost("CreateProduct")]
 public async Task<ActionResult<Product>> CreateProduct(Product product)
 {
   _repo.AddData(product);
   

   if(await _repo.SaveAllDataAsync())
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
   _repo.Edit(product);
    }

    if(await _repo.SaveAllDataAsync())
    {
     return NoContent();
    }else{
      return BadRequest("Oops,there's a problem updating the product");
    }
 }

[HttpDelete("id:int")]
public async Task<ActionResult> DeleteProducts(int id)
{
 var deleteProduct = await _repo.GetDataByIdAsync(id);
 if(deleteProduct==null)
 {
 return NotFound("Oops, object notfound");
 }
 _repo.DeleteData(deleteProduct);
 if(await _repo.SaveAllDataAsync()){
  return NotFound();
 }else
 {
  return BadRequest("Oops, sorry we can't delete this product, maybe something went wrong");
 }
}
 // check if the product already exist in database
 private bool ProductAlreadyExist(int id)
 {
  return _repo.Exist(id);
 }
}
