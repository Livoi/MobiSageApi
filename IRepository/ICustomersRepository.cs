using MobiSageApi.Models;
using Pastel.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.IRepository
{
    public interface ICustomersRepository
    {
        Task<List<CustomerModel>> GetCustomers();
        Task<Customer> CreateCustomer(CustomerModel cm);
        Task<Customer> UpdateCustomer(CustomerModel cm);
    }
}
