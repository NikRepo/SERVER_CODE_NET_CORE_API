using CommonServiceHelper.Service_Model;
using ProductService.Interfaces;

namespace ProductService
{
    public class ProductBuilder : IProductRespository
    {
        public List<ProductModel> Products { get; set; }
        public ProductBuilder()
        {
            Products = new List<ProductModel>
            {
                new ProductModel { Id = "1" , Title = "Bat" , Category = "Sport" , Price = "200"},
                new ProductModel { Id = "2" , Title = "Soap" , Category = "Consumer" , Price = "20"},
                new ProductModel { Id = "3" , Title = "Ear Phone" , Category = "Electronics" , Price = "150"},
                new ProductModel { Id = "4" , Title = "Washing Machine" , Category = "Home Applience" , Price = "20000"}
            };
        }
        public List<ProductModel> GetAllProducts()
        {
            return Products;
        }

        public ProductModel GetProduct(string id)
        {
            if(!string.IsNullOrEmpty(id) && Products.Any(n=>n.Id==id))
            {
                return Products.Where(n => n.Id == id).Select(n => n).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }


        public ProductModel AddProduct(ProductModel product)
        {
            if (product != null && Products.Any(n => n.Id == product.Id))
            {
                Products.Add(product);
                return product;
            }
            else
            {
                return null;
            }
        }
    }
}
