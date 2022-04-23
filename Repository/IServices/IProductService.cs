using CNaturalApi.Models;

namespace CNaturalApi.Repository.IServices
{
    public interface IProductService 
    {
        public Task<Product> GetProduct(int productId);
        public  Task<IEnumerable<Product>> GetAllProducts();
        public  Task<Product> AddProduct(Product product);
        public  Task<Product> UpdateProduct(int id, Product product);
        public Task<bool> DeleteProduct(int productId);
        public Task<bool> ProductAlreadyExists(string name);
        
        
    }
    
}
