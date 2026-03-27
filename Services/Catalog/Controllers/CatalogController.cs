using Catalog.Commands.Products;
using Catalog.DTOs;
using Catalog.Mappers;
using Catalog.Queries.Brands;
using Catalog.Queries.Products;
using Catalog.Queries.Types;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;
    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Products")]
    public async Task<ActionResult<Pagination<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
    {
        var query = new GetAllProductsQuery(catalogSpecParams);
        var result = await _mediator.Send(query);
        return Ok(result.Map(p => p.toDto()));
    }

    [HttpGet("Products/{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result.toDto());
    }

    [HttpGet("Products/Name/{productName}")]
    public async Task<ActionResult<IList<ProductDto>>> GetProductByName([FromRoute] string productName)
    {
        var query = new GetProductsByNameQuery(productName);
        var result = await _mediator.Send(query);
        if (result == null || !result.Any())
        {
            return NotFound();
        }

        var dtoList = result.Select(p => p.toDto()).ToList();

        return Ok(dtoList);
    }

    [HttpPost("Products")]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
    {
        var result = await _mediator.Send(createProductCommand);
        return Ok(result);
    }

    [HttpDelete("Products/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("Products/{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdateProductDto updateProductDto)
    {
        var command = new UpdateProductCommand(
            id,
            updateProductDto.Name,
            updateProductDto.Summary,
            updateProductDto.Description,
            updateProductDto.ImageFile,
            updateProductDto.BrandId,
            updateProductDto.TypeId,
            updateProductDto.Price
        );

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("Brands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
    {
        var query = new GetAllBrandsQuery();
        var result= await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("Types")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
    {
        var query=new GetAllTypesQuery();
        var result= await _mediator.Send(query);
        return Ok(result);
    }

 





}
