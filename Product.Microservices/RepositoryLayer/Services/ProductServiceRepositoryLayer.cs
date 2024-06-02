using Customer.Microservice.RepositoryLayer.Context;
using Customer.Microservice.RepositoryLayer.Entity;
using Customer.Microservice.RepositoryLayer.Interface;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Customer.Microservice.RepositoryLayer.Services
{
    public class ProductServiceRepositoryLayer : IProductRepositoryLayer
    {
        private readonly DapperContext _context;

        public ProductServiceRepositoryLayer(DapperContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity> Create(ProductEntity productEntity)
        {
            var query = "spCreateProduct";
            var parameters = new DynamicParameters();
            parameters.Add("Name", productEntity.Name);
            parameters.Add("Rate", productEntity.Rate);

            using (var connection = _context.CreateConnection())
            {
                productEntity.Id = await connection.QuerySingleAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                return productEntity;
            }
        }

        public async Task<string> Delete(ProductEntity productEntity)
        {
            var query = "spDeleteProduct";
            var parameters = new DynamicParameters();
            parameters.Add("Id", productEntity.Id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return $"Product with Id {productEntity.Id} has been deleted.";
            }
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            var query = "spGetAllProducts";

            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<ProductEntity>(query, commandType: CommandType.StoredProcedure);
                return products;
            }
        }

        public async Task<ProductEntity> GetById(int id)
        {
            var query = "spGetProductById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QuerySingleOrDefaultAsync<ProductEntity>(query, parameters, commandType: CommandType.StoredProcedure);
                return product;
            }
        }

        public async Task<ProductEntity> Update(ProductEntity productEntity)
        {
            var query = "spUpdateProduct";
            var parameters = new DynamicParameters();
            parameters.Add("Id", productEntity.Id);
            parameters.Add("Name", productEntity.Name);
            parameters.Add("Rate", productEntity.Rate);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return productEntity;
            }
        }
    }
}
