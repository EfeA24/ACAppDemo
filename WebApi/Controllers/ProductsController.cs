using AutoMapper;
using DataTransferObjects.ProductDto;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Contrats;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IMapper _mapper;

        public ProductsController(IServiceManager service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.ProductService.GetAllProductsAsync();
            var dto = _mapper.Map<IEnumerable<MainProductDto>>(products);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.ProductService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var dto = _mapper.Map<GetProductDto>(product);
            return Ok(dto);
        }

        [HttpPost("add-by-barcode")]
        public async Task<IActionResult> AddByBarcode([FromBody] AddProductDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.ProductBarcode))
                return BadRequest("Geçersiz veri.");

            var product = _mapper.Map<Product>(dto);
            var result = await _service.ProductService.AddOrUpdateProductAsync(product);
            var resultDto = _mapper.Map<GetProductDto>(result);
            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (dto == null)
                return BadRequest("Geçersiz veri.");

            var product = _mapper.Map<Product>(dto);
            var result = await _service.ProductService.AddOrUpdateProductAsync(product);
            var resultDto = _mapper.Map<GetProductDto>(result);
            return Ok(resultDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto dto)
        {
            if (dto == null)
                return BadRequest("Eksik veri.");

            var product = _mapper.Map<Product>(dto);
            var result = await _service.ProductService.UpdateProductAsync(product);
            var resultDto = _mapper.Map<GetProductDto>(result);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.ProductService.DeleteProductAsync(id);
            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
