using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSTask.Models.Dtos;
using NSTask.Services;

namespace NSTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.ShowList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.ShowProduct(id);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(AddProductDto dto)
        {
            var userId = User.Claims.First(p => p.Type == "UserId").Value;

            var result = await _service.AddProduct(dto, userId);
            if (result != null && result == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditProductDto dto)
        {
            var userId = User.Claims.First(p => p.Type == "UserId").Value;

            var result = await _service.EditProduct(id, dto, userId);

            if (result != null && result == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, RemoveProductDto dto)
        {
            var userId = User.Claims.First(p => p.Type == "UserId").Value;

            var result = await _service.RemoveProduct(id, dto, userId);

            if (result != null && result == true)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
