using Microsoft.EntityFrameworkCore;
using NSTask.Data;
using NSTask.Models.Dtos;
using NSTask.Models.Entities;

namespace NSTask.Services
{
    public class ProductService:IProductService
    {
        private readonly NSDataBase _DbContext;

        public ProductService(NSDataBase Dbcontext)
        {
            _DbContext = Dbcontext;
        }

        public async Task<bool> AddProduct(AddProductDto dto, string userId)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(p => p.Id.ToString() == userId);

            var newProduct = new Product
            {
                IsAvailable = dto.IsAvailable,
                Name = dto.Name,
                ProduceDate = DateTime.Now,
                ManufactureEmail = user.Email,
                ManufacturePhone = user.PhoneNumber,
            };

            await _DbContext.AddAsync<Product>(newProduct);
            await _DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditProduct(int id, EditProductDto dto, string userId)
        {
            var product = await _DbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            var user = await _DbContext.Users.FirstOrDefaultAsync(p => p.Id.ToString() == userId);

            if (user != null && product != null)
            {
                if (product.ManufacturePhone == user.PhoneNumber && product.ManufactureEmail == user.PhoneNumber)
                {
                    if (dto.IsAvailable != null)
                    {
                        product.IsAvailable = dto.IsAvailable;
                    }
                    if (dto.Name != null)
                    {
                        product.Name = dto.Name;
                    }

                    _DbContext.Products.Update(product);
                    _DbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveProduct(int id, RemoveProductDto dto, string userId)
        {
            var product = await _DbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            var user = await _DbContext.Users.FirstOrDefaultAsync(p => p.Id.ToString() == userId);

            if (user != null && product != null)
            {
                if (product.ManufacturePhone == user.PhoneNumber && product.ManufactureEmail == user.PhoneNumber)
                {
                    _DbContext.Products.Remove(product);
                    _DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ShowProductDto>> ShowList()
        {
            var products = await _DbContext.Products.ToListAsync();

            var productList = new List<ShowProductDto>();

            foreach (var item in products)
            {
                var product = new ShowProductDto
                {
                    IsAvailable = item.IsAvailable,
                    Name = item.Name,
                    ProduceDate = item.ProduceDate
                };

                productList.Add(product);
            }

            return productList;
        }

        public async Task<ShowProductDto> ShowProduct(int id)
        {
            var product = await _DbContext.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                var productResult = new ShowProductDto
                {
                    IsAvailable = product.IsAvailable,
                    Name = product.Name,
                    ProduceDate = product.ProduceDate,
                    IsSuccest = true,

                };

                return productResult;
            }
            else
            {
                var productResult = new ShowProductDto
                {
                    IsSuccest = false,
                };

                return productResult;
            }
        }
    }
}
