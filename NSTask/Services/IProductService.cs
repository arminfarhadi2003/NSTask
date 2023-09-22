using NSTask.Models.Dtos;

namespace NSTask.Services
{
    public interface IProductService
    {
        public Task<List<ShowProductDto>> ShowList();
        public Task<bool> AddProduct(AddProductDto dto, string userId);
        public Task<bool> RemoveProduct(int id, RemoveProductDto dto, string userId);
        public Task<ShowProductDto> ShowProduct(int id);
        public Task<bool> EditProduct(int id, EditProductDto dto, string userId);
    }
}
