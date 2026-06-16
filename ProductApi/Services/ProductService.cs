using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService
    {
        private static List<Product> _products = new List<Product>();
        private static int _nextId = 1;

        public List<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public Product Add(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return product;
        }
    }
}