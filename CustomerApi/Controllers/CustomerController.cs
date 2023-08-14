using Microsoft.AspNetCore.Mvc;
using CustomerApi.Dtos;
using CustomerApi.Repositories;
using CustomerApi.UseCases;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomersContext _customersContext;
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;

        public CustomerController(CustomersContext customersContext,
            IUpdateCustomerUseCase updateCustomersUseCase)
        {
            _customersContext = customersContext;
            _updateCustomerUseCase = updateCustomersUseCase;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerDto>))]
        public async Task<IActionResult> GetCustomers()
        {
            var result = _customersContext.Customers
                .Select(c=>c.ToDto()).ToList();

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer(long id) 
        {
            CustomerEntity result = await _customersContext.Get(id);

            // Check if result is null
            if (result == null)
            {
                return NotFound("Customer not found.");
            }

            return new OkObjectResult(result.ToDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var result = await _customersContext.Delete(id);

            return new OkObjectResult(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await _customersContext.Add(customer);

            return new CreatedResult($"https://localhost: 7270/api/customer/{result.Id}", null);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await _updateCustomerUseCase.Execute(customer);

            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

    }
}
