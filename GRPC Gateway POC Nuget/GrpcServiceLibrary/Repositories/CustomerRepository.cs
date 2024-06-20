using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcServiceLibrary.Models;

namespace GrpcServiceLibrary.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe" },
                new Customer { Id = 2, Name = "Jane Smith" }
            };
        }

        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return Task.FromResult<IEnumerable<Customer>>(_customers);
        }

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(customer);
        }
    }
}
