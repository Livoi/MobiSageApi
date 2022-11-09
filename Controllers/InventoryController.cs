using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobiSageApi.IRepository;
using MobiSageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobiSageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IConfiguration _config;
        IInventoryRepository _oInventoryRepository = null;

        public InventoryController(IConfiguration config, IInventoryRepository oInventoryRepository)
        {
            _config = config;
            _oInventoryRepository = oInventoryRepository;
        }

        [HttpGet]
        [Route("GetInventoryItems")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var list = await _oInventoryRepository.GetInventory();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }        

    }
}
