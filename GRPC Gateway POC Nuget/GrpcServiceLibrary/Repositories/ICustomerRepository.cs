using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcServiceLibrary.Models;

namespace GrpcServiceLibrary.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
