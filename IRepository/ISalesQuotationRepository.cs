using MobiSageApi.Models;
using Pastel.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.IRepository
{
    public interface ISalesQuotationRepository
    {
        Task<object> GetSalesQuotations();
        Task<SalesOrderQuotation> CreateSalesQuote(SaleQuotationModel sm);
    }
}
