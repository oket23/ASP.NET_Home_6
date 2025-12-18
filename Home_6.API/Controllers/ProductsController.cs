using Home_6.API.Interfaces;
using Home_6.API.Requests;
using Home_6.BLL.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Home_6.API.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;

    public ProductsController(IProductsService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductProperties properties)
    {
        var result = await _service.GetAllAsync(properties);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(result); 
    }
    
    [HttpPatch("{id}/state")]
    public async Task<IActionResult> ChangeState(int id, [FromBody] ChangeStateProductRequest request)
    {
        var result = await _service.ChangeStateAsync(id, request);
        return Ok(result);
    }
}