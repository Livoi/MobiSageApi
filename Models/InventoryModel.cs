using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.Models
{
    public class InventoryModel
    {  
        public int StockLink { get; set; }
        public string Code { get; set; }
        public string Description_1 { get; set; }
        public string Description_2 { get; set; }
        public string Description_3 { get; set; }
        public bool ItemActive { get; set; }
    }
}
