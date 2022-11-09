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
    public class InventoryRepository : IInventoryRepository
    {
        List<InventoryModel> _oInventories= new List<InventoryModel>();
        private string Database, CommonDB, Instance, UserName, Password, SdkUserName, SdkPassword = string.Empty;
        public InventoryRepository(IConfiguration configuration)
        {
            Database = configuration.GetValue<string>("AppSettings:SageSdk:Database");
            CommonDB = configuration.GetValue<string>("AppSettings:SageSdk:CommonDB");
            Instance = configuration.GetValue<string>("AppSettings:SageSdk:Instance");
            UserName = configuration.GetValue<string>("AppSettings:SageSdk:UserName");
            Password = configuration.GetValue<string>("AppSettings:SageSdk:Password");
            SdkUserName = configuration.GetValue<string>("AppSettings:SageSdk:SdkUserName");
            SdkPassword = configuration.GetValue<string>("AppSettings:SageSdk:SdkPassword");
            //DatabaseContext.Initialise("Imports", "EV", "192.168.0.143", "sa", "Kens@1234", "DE12111058", "2523370");

            DatabaseContext.Initialise(Database, CommonDB, Instance, UserName, Password, SdkUserName, SdkPassword);
        }

        public async Task<List<InventoryModel>> GetInventory()
        {

            _oInventories = new List<InventoryModel>();       
            var criteria = $"StockLink > {"1"}";
            var inventory = InventoryItem.List(criteria);
         
            foreach (DataRow matchP in inventory.Rows)
            {
                _oInventories.Add(
                    new InventoryModel
                    {
                        StockLink = Convert.ToInt32(matchP["StockLink"]),
                        Code = matchP["Code"].ToString(),
                        Description_1 = matchP["Description_1"].ToString(),
                        Description_2 = matchP["Description_2"].ToString(),
                        Description_3 = matchP["Description_3"].ToString(),
                        ItemActive = Convert.ToBoolean(matchP["ItemActive"])
                    }) ;                
            }
            return _oInventories;
        }

        //public async Task <Customer> CreateCustomer(InventoryModel cm)
        //{        
        //    Customer customer = new Customer();
        //    customer.Code =cm.Account.ToString();
        //    customer.Description = cm.Name.ToString();
        //    customer.Telephone = cm.Telephone.ToString();
        //    customer.EmailAddress = cm.Email.ToString();
        //    customer.TaxNumber = cm.TaxNumber.ToString();
        //    customer.PostalAddress.Line1 = cm.Address1.ToString();
        //    customer.ContactPerson = cm.ContactPerson.ToString();
        //    customer.Telephone2 = cm.Telephone2.ToString();
        //    customer.PostalAddress.Line2 = cm.Address2.ToString();
        //    customer.Save();

        //    return customer;

        //}
        //public async Task<Customer> UpdateCustomer(InventoryModel cm)
        //{
        //    Customer customer = Customer.GetByCode(cm.Account);           
        //    customer.Description = cm.Name.ToString();
        //    customer.Telephone = cm.Telephone.ToString();
        //    customer.EmailAddress = cm.Email.ToString();
        //    customer.TaxNumber = cm.TaxNumber.ToString();
        //    customer.PostalAddress.Line1 = cm.Address1.ToString();
        //    customer.ContactPerson = cm.ContactPerson.ToString();
        //    customer.Telephone2 = cm.Telephone2.ToString();
        //    customer.PostalAddress.Line2 = cm.Address2.ToString();
        //    customer.Save();

        //    return customer;

        //}


    }
}
