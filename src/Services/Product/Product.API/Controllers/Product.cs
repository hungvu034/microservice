using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Infrastructure.Common ; 
using Product.API.Entities ; 
using Product.API.Persistence ;
using Contracts.Common.Interfaces;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Product;
using AutoMapper;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IMapper _mapper ; 
        private readonly IProductRepository _repository ; 
        public ProductController(ILogger<ProductController> logger , IProductRepository repository , IMapper mapper)
        {
            _logger = logger;
            _repository = repository ; 
            _mapper = mapper ; 
        }



        #region CRUD
        [HttpGet()]
        public JsonResult GetProducts(){
            IEnumerable<CatalogProduct> items = _repository.FindAll();
            var result = _mapper.Map<IEnumerable<ProductDto>>(items);
            return Json(result);
        }
        [HttpGet("{id}")]
        public JsonResult GetProductByID([RequiredAttribute()]long id){
            CatalogProduct product = _repository.FindByCondition(x => x.Id == id).FirstOrDefault();
            return Json(product);
        }

        // [HttpPost()]
        // public async Task<IActionResult> PostProduct(ProductDto productDto){
        //     CatalogProduct newProduct = new CatalogProduct();
        //     foreach(var CatalogProperty in newProduct.GetType().GetProperties()){
        //         Type DtoType = productDto.GetType();
        //         PropertyInfo DtoProperty = DtoType.GetProperty(CatalogProperty.Name);
        //         if(DtoProperty != null){
        //            var value = DtoProperty.GetValue(productDto);
        //            CatalogProperty.SetValue(newProduct , value);
        //         }
        //     }
        //   long id = await _repository.CreateAsync(newProduct);
        //   await _repository.SaveChangesAsync();
        //    return Ok(id);
        // }
        
        [HttpPost()]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto){
            CatalogProduct product = _mapper.Map<CatalogProduct>(createProductDto);
            await _repository.CreateProduct(product);
            await _repository.SaveChangesAsync();
            ProductDto productDto = _mapper.Map<ProductDto>(product); 
            return Ok(productDto); 
        }
        
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct([Required()]long id ,[FromBody] UpdateProductDto productDto){
                CatalogProduct oldProduct = _repository.FindByCondition( x => x.Id == id).FirstOrDefault();
                _logger.LogInformation(id + ""); 
                if(oldProduct == null){
                    return NotFound();
                }
                CatalogProduct newProduct = _mapper.Map(productDto , oldProduct);
                await _repository.UpdateProduct(newProduct);
                ProductDto result = _mapper.Map<ProductDto>(newProduct);
                return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long Id){
            CatalogProduct productDelete =  await _repository.GetByIdAsync(Id);
            await _repository.DeleteProduct(productDelete);
            await _repository.SaveChangesAsync();
            return Ok(productDelete.Id);
        }
        #endregion 

    }
}