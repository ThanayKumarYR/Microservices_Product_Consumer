using Customer.Microservice.RepositoryLayer.Context;
using Customer.Microservice.RepositoryLayer.Entity;
using Customer.Microservice.RepositoryLayer.Interface;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Customer.Microservice.RepositoryLayer.Services
{
    public class CustomerServiceRepositoryLayer : ICustomerRepositoryLayer
    {
        private readonly DapperContext _context;

        public CustomerServiceRepositoryLayer(DapperContext context)
        {
            _context = context;
        }

        public async Task<CustomerEntity> Create(CustomerEntity customerEntity)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "spCreateCustomer";
                var parameters = new DynamicParameters();
                parameters.Add("Name", customerEntity.Name);
                parameters.Add("Contact", customerEntity.Contact);
                parameters.Add("City", customerEntity.City);
                parameters.Add("Email", customerEntity.Email);

                var id = await connection.QuerySingleAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                customerEntity.Id = id; // Assuming CustomerEntity has an Id property
                return customerEntity;
            }
        }

        public async Task<IEnumerable<CustomerEntity>> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "spGetAllCustomers";
                var customers = await connection.QueryAsync<CustomerEntity>(query, commandType: CommandType.StoredProcedure);
                return customers;
            }
        }

        public async Task<CustomerEntity> GetById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "spGetCustomerById";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);

                var customer = await connection.QuerySingleOrDefaultAsync<CustomerEntity>(query, parameters, commandType: CommandType.StoredProcedure);
                return customer;
            }
        }

        public async Task<CustomerEntity> Update(CustomerEntity customerEntity)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "spUpdateCustomer";
                var parameters = new DynamicParameters();
                parameters.Add("Id", customerEntity.Id); // Assuming CustomerEntity has an Id property
                parameters.Add("Name", customerEntity.Name);
                parameters.Add("Contact", customerEntity.Contact);
                parameters.Add("City", customerEntity.City);
                parameters.Add("Email", customerEntity.Email);

                await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return customerEntity;
            }
        }

        public async Task<string> Delete(CustomerEntity customerEntity)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "spDeleteCustomer";
                var parameters = new DynamicParameters();
                parameters.Add("Id", customerEntity.Id); // Assuming CustomerEntity has an Id property

                await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return $"Customer with Id {customerEntity.Id} has been deleted.";
            }
        }
    }
}
