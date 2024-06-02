using Customer.Microservice.RepositoryLayer.Entity;
namespace Customer.Microservice.RepositoryLayer.Interface
{
    public interface IProductRepositoryLayer
    {
        public Task<ProductEntity> Create(ProductEntity productEntity);
        public Task<IEnumerable<ProductEntity>> GetAll();
        public Task<ProductEntity> GetById(int id);
        public Task<ProductEntity> Update(ProductEntity productEntity);
        public Task<string> Delete(ProductEntity productEntity);
    }
}
