using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CustomerApi.Dtos;


namespace CustomerApi.Repositories
{
    public class CustomersContext : DbContext
    {

        public CustomersContext(DbContextOptions<CustomersContext> options) 
            : base(options)
        {

        }
       public DbSet<CustomerEntity> Customers { get; set; }

       public async Task<CustomerEntity?> Get(long id)
        {
            return await Customers.FirstOrDefaultAsync(X => X.Id == id);
        }

        public async Task<bool> Delete(long id)
        {
            CustomerEntity entity = await Get(id);
            Customers.Remove(entity);
            SaveChanges();
            return true;    
        }

        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                Address = customerDto.Address,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
            };


            EntityEntry<CustomerEntity> response = await Customers.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("could not save"));
        }

        public async Task<bool> update(CustomerEntity customerEntity)
        {
           this.Update(customerEntity);
            await SaveChangesAsync();

            return true;
        }
    }


    public class CustomerEntity
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }


        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Id = Id ?? throw new Exception("the id could not be null")
            };
       }
    }
}


