using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobiSageApi.IRepository;
using MobiSageApi.Models;
using Pastel.Evolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobiSageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesQuotationController : ControllerBase
    {
        private IConfiguration _config;
        ISalesQuotationRepository _oISalesQuotationRepository = null;

        public SalesQuotationController(IConfiguration config, ISalesQuotationRepository oISalesQuotationRepository)
        {
            _config = config;
            _oISalesQuotationRepository = oISalesQuotationRepository;
        }

        [HttpPost]
        [Route("CreateSalesQuote")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomers([FromBody] SaleQuotationModel sm)
        {
            try
            {
                SalesOrderQuotation quote = await _oISalesQuotationRepository.CreateSalesQuote(sm);
                return Ok(quote.QuoteNo);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSaleQuotes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var quotes = await _oISalesQuotationRepository.GetSalesQuotations();
                return Ok(quotes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
