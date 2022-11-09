using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.Models
{
    public class SaleQuotationModel
    {
        public int CustID { get; set; }
        public int StockLink { get; set; }
        public int Quantity { get; set; }
        public double UnitSellingPrice { get; set; }

    }
}
