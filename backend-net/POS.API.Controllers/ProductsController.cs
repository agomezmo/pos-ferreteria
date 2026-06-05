using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.DTOs;
using POS.API.Services;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetProducts([FromQuery] bool includeInactive = false)
    {
        var products = await _productService.GetProductsAsync(includeInactive);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound(new { error = "Producto no encontrado" });
        return Ok(product);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ProductDTO>>> SearchProducts([FromQuery] string q)
    {
        if (string.IsNullOrEmpty(q))
            return await GetProducts();
        var products = await _productService.SearchProductsAsync(q);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductRequest request)
    {
        var result = await _productService.CreateProductAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
    {
        var result = await _productService.UpdateProductAsync(id, request);
        if (result == null)
            return NotFound(new { error = "Producto no encontrado" });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (!result)
            return NotFound(new { error = "Producto no encontrado" });
        return Ok(new { message = "Producto desactivado" });
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
    {
        var categories = await _productService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpPost("categories")]
    public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CreateCategoryRequest request)
    {
        var result = await _productService.CreateCategoryAsync(request);
        return Ok(result);
    }

    [HttpGet("suppliers")]
    public async Task<ActionResult<List<SupplierDTO>>> GetSuppliers()
    {
        var suppliers = await _productService.GetSuppliersAsync();
        return Ok(suppliers);
    }

    [HttpPost("suppliers")]
    public async Task<ActionResult<SupplierDTO>> CreateSupplier([FromBody] CreateSupplierRequest request)
    {
        var result = await _productService.CreateSupplierAsync(request);
        return Ok(result);
    }
}
