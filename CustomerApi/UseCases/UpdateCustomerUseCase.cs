using CustomerApi.Repositories;

namespace CustomerApi.UseCases
{
    public interface IUpdateCustomerUseCase
    {
        Task<Dtos.CustomerDto?> Execute(Dtos.CustomerDto customer);
    }

    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {

        private readonly CustomersContext _customersContext;

        public UpdateCustomerUseCase(CustomersContext customersContext)
        {
            _customersContext = customersContext;
        }


        public async Task<Dtos.CustomerDto?> Execute(Dtos.CustomerDto customer)
        {
            var entity = await _customersContext.Get(customer.Id);

            if (entity == null)
                return null;

                entity.FirstName = customer.FirstName;
                entity.LastName = customer.LastName;
                entity.Email = customer.Email;
                entity.Phone = customer.Phone;
                entity.Address = customer.Address;

            await _customersContext.SaveChangesAsync();
                return entity.ToDto();
            
        }
    }
}

