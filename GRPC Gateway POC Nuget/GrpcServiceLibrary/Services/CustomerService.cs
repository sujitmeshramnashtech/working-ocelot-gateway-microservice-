using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServiceLibrary.Protos; // Ensure this namespace is correct
using GrpcServiceLibrary.Repositories;
using Microsoft.Extensions.Logging;

namespace GrpcServiceLibrary.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public override async Task<GetCustomersResponse> GetCustomers(GetCustomersRequest request, ServerCallContext context)
        {
            var customers = await _customerRepository.GetCustomersAsync();
            var response = new GetCustomersResponse();
            response.Customers.AddRange(customers.Select(c => new CustomerModel { Id = c.Id, Name = c.Name }));

            return response;
        }

        public override async Task<GetCustomerByIdResponse> GetCustomerById(GetCustomerByIdRequest request, ServerCallContext context)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.Id);
            if (customer == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Customer not found"));
            }

            var response = new GetCustomerByIdResponse
            {
                Customer = new CustomerModel { Id = customer.Id, Name = customer.Name }
            };

            return response;
        }
    }
}
