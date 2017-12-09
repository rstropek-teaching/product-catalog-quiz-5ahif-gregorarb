using ProductCatalog.Models;
using System.Collections.Generic;

namespace ProductCatalog.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void DeleteById(int id);

        void AddProduct(Product product);

        IEnumerable<Product> SearchProduct(string name);
    }
}
