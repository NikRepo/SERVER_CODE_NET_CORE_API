using CommonServiceHelper.Service_Model;

namespace ProductService.Interfaces
{
    public interface IProductRespository
    {
        public List<ProductModel> GetAllProducts();

        public ProductModel GetProduct(string id);

        public ProductModel AddProduct(ProductModel product);
    }
}
