using ECommerceAPI.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async void Added()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new Domain.Entities.Product() {Id = Guid.NewGuid(), Name = "Product-1", Price = 100, CreateDate = DateTime.UtcNow, Stock = 10},
                new Domain.Entities.Product() {Id = Guid.NewGuid(), Name = "Product-2", Price = 200, CreateDate = DateTime.UtcNow, Stock = 20}
            });
            await _productWriteRepository.SaveAsync();
        }
    }
}
