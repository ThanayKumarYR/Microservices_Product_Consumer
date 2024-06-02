using Customer.Microservice.RepositoryLayer.Entity;
namespace Customer.Microservice.RepositoryLayer.Interface
{
    public interface ICustomerRepositoryLayer
    {
        public Task<CustomerEntity> Create(CustomerEntity customerEntity);
        public Task<IEnumerable<CustomerEntity>> GetAll();
        public Task<CustomerEntity> GetById(int id);
        public Task<CustomerEntity> Update(CustomerEntity customerEntity);
        public Task<string> Delete(CustomerEntity customerEntity);
    }
}
