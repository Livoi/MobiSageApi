using MobiSageApi.Models;
using Pastel.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiSageApi.IRepository
{
    public interface IInventoryRepository
    {
        Task<List<InventoryModel>> GetInventory();
    }
}
