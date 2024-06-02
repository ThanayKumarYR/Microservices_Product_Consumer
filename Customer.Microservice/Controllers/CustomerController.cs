using Microsoft.AspNetCore.Mvc;
using Customer.Microservice.RepositoryLayer.Entity;
using Customer.Microservice.RepositoryLayer.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customer.Microservice.ModelLayer;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepositoryLayer _customerRepository;

        public CustomerController(ICustomerRepositoryLayer customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<CustomerEntity>>>> GetAllCustomers()
        {
            var response = new ResponseModel<IEnumerable<CustomerEntity>>();
            try
            {
                var customers = await _customerRepository.GetAll();
                response.Success = true;
                response.Data = customers;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<CustomerEntity>>> GetCustomerById(int id)
        {
            var response = new ResponseModel<CustomerEntity>();
            try
            {
                var customer = await _customerRepository.GetById(id);
                if (customer == null)
                {
                    response.Success = false;
                    response.Message = "Customer not found";
                    return NotFound(response);
                }
                response.Success = true;
                response.Data = customer;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<ResponseModel<CustomerEntity>>> CreateCustomer(CustomerEntity customerEntity)
        {
            var response = new ResponseModel<CustomerEntity>();
            try
            {
                var createdCustomer = await _customerRepository.Create(customerEntity);
                response.Success = true;
                response.Data = createdCustomer;
                return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return BadRequest(response);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<CustomerEntity>>> UpdateCustomer(int id, CustomerEntity customerEntity)
        {
            var response = new ResponseModel<CustomerEntity>();
            if (id != customerEntity.Id)
            {
                response.Success = false;
                response.Message = "Customer ID mismatch";
                return BadRequest(response);
            }

            try
            {
                var updatedCustomer = await _customerRepository.Update(customerEntity);
                if (updatedCustomer == null)
                {
                    response.Success = false;
                    response.Message = "Customer not found";
                    return NotFound(response);
                }
                response.Success = true;
                response.Data = updatedCustomer;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteCustomer(int id)
        {
            var response = new ResponseModel<string>();
            try
            {
                var customer = await _customerRepository.GetById(id);
                if (customer == null)
                {
                    response.Success = false;
                    response.Message = "Customer not found";
                    return NotFound(response);
                }

                await _customerRepository.Delete(customer);
                response.Success = true;
                response.Data = $"Customer with Id {id} has been deleted.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
