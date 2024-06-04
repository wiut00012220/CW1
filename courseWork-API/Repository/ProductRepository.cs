using courseWork_API.DBContexts;
using courseWork_API.Model;
using System.Data.Entity;

namespace courseWork_API.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductContext _dbContext;
        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            /*var product = _dbContext.Products.Find(productId);
            _dbContext.Products.Remove(product);
            Save();*/
            var product = _dbContext.Products.Find(productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                Save();
            }
        }
        public Product GetProductById(int productId)
        {
            return _dbContext.Products.Find(productId); ;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }
        public void InsertProduct(Product product)
        {

            _dbContext.Add(product);
            Save();
        }
        
        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }

    }
}
