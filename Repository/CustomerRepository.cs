using Microsoft.Extensions.Configuration;
using MobiSageApi.IRepository;
using MobiSageApi.Models;
using Pastel.Evolution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.Repository
{
    public class CustomerRepository : ICustomersRepository
    {
        List<CustomerModel> _oCustomers= new List<CustomerModel>();
        private string Database, CommonDB, Instance, UserName, Password, SdkUserName, SdkPassword = string.Empty;
        public CustomerRepository(IConfiguration configuration)
        {
            Database = configuration.GetValue<string>("AppSettings:SageSdk:Database");
            CommonDB = configuration.GetValue<string>("AppSettings:SageSdk:CommonDB");
            Instance = configuration.GetValue<string>("AppSettings:SageSdk:Instance");
            UserName = configuration.GetValue<string>("AppSettings:SageSdk:UserName");
            Password = configuration.GetValue<string>("AppSettings:SageSdk:Password");
            SdkUserName = configuration.GetValue<string>("AppSettings:SageSdk:SdkUserName");
            SdkPassword = configuration.GetValue<string>("AppSettings:SageSdk:SdkPassword");

            DatabaseContext.Initialise(Database, CommonDB, Instance, UserName, Password, SdkUserName, SdkPassword);
        }

        public async Task<List<CustomerModel>> GetCustomers()
        {
          
            _oCustomers = new List<CustomerModel>();

            string criteria = $"dTimeStamp > '{"2021-01-01"}'";
            var criteria_ = $"StockLink > {"1"}";        

            var customers = Customer.List(criteria);
            //var inventory = InventoryItem.List(criteria_);
            /*List<DataRow> list = Customer.List(criteria).AsEnumerable().ToList();*/
            foreach (DataRow matchP in customers.Rows)
            {
                _oCustomers.Add(
                    new CustomerModel
                    {
                        DCLink = Convert.ToInt32(matchP["DCLink"]),
                        Account = matchP["Account"].ToString(),
                        Name = matchP["Name"].ToString(),
                        Address1 = matchP["Physical1"].ToString(),
                        Address2 = matchP["Physical2"].ToString(),
                        ContactPerson = matchP["Contact_Person"].ToString(),
                        Telephone = matchP["Telephone"].ToString(),
                        Title = matchP["Title"].ToString()
                    }) ;                
            }
            return _oCustomers;
        }

        public async Task <Customer> CreateCustomer(CustomerModel cm)
        {        
            Customer customer = new Customer();
            customer.Code =cm.Account.ToString();
            customer.Description = cm.Name.ToString();
            customer.Telephone = cm.Telephone.ToString();
            customer.EmailAddress = cm.Email.ToString();
            customer.TaxNumber = cm.TaxNumber.ToString();
            customer.PostalAddress.Line1 = cm.Address1.ToString();
            customer.ContactPerson = cm.ContactPerson.ToString();
            customer.Telephone2 = cm.Telephone2.ToString();
            customer.PostalAddress.Line2 = cm.Address2.ToString();
            customer.Save();

            return customer;

        }
        public async Task<Customer> UpdateCustomer(CustomerModel cm)
        {
            Customer customer = Customer.GetByCode(cm.Account);           
            customer.Description = cm.Name.ToString();
            customer.Telephone = cm.Telephone.ToString();
            customer.EmailAddress = cm.Email.ToString();
            customer.TaxNumber = cm.TaxNumber.ToString();
            customer.PostalAddress.Line1 = cm.Address1.ToString();
            customer.ContactPerson = cm.ContactPerson.ToString();
            customer.Telephone2 = cm.Telephone2.ToString();
            customer.PostalAddress.Line2 = cm.Address2.ToString();
            customer.Save();

            return customer;

        }


    }
}
