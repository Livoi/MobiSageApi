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
    public class SalesQuotationRepository : ISalesQuotationRepository
    {
        List<SaleQuotationModel> _oQuotations = new List<SaleQuotationModel>();
        private string Database, CommonDB, Instance, UserName, Password, SdkUserName, SdkPassword = string.Empty;

        public SalesQuotationRepository(IConfiguration configuration)
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


        public async Task <SalesOrderQuotation> CreateSalesQuote(SaleQuotationModel sm)
        {           
            SalesOrderQuotation SOQ = new SalesOrderQuotation();
            SOQ.Customer = new Customer(sm.CustID);

            OrderDetail OD = new OrderDetail();
            SOQ.Detail.Add(OD);
            //Vaious Order Detail properties can be added like warehouse , sales reps , userfields etc
            OD.InventoryItem = new InventoryItem(sm.StockLink);//Use the inventoryItem constructor to specify a Item
            OD.Quantity = sm.Quantity;
            OD.ToProcess = OD.Quantity;
            OD.UnitSellingPrice = sm.UnitSellingPrice;

            SOQ.Save();//You can Save the quote or process a existing quote in to a invoice      

            return SOQ;

        }

        public async Task<object> GetSalesQuotations()
        {

            _oQuotations = new List<SaleQuotationModel>();
            var criteria = $"StockLink > {"1"}";
            var inventory = SalesOrderQuotation.List(criteria);
            
            return inventory;
        }


    }
}
