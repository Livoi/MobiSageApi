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
    public class CustomerController : ControllerBase
    {
        private IConfiguration _config;
        ICustomersRepository _oCustomerRepository = null;

        public CustomerController(IConfiguration config, ICustomersRepository oCustomerRepository)
        {
            _config = config;
            _oCustomerRepository = oCustomerRepository;
        }

        [HttpGet]
        [Route("GetCustomers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var list = await _oCustomerRepository.GetCustomers();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //This action at /Customer/IndexFromBody can bind JSON 
        [HttpPost]
        [Route("CreateCustomer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel cm)
        {
            try
            {
                var result = await _oCustomerRepository.CreateCustomer(cm);
                cm.DCLink = result.ID;
                return Ok(cm.DCLink);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //This action at /Customer/IndexFromBody can bind JSON 
        [HttpPost]
        [Route("UpdateCustomer")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel cm)
        {
            try
            {
                var result = await _oCustomerRepository.UpdateCustomer(cm);
                cm.DCLink = result.ID;
                return Ok(cm.DCLink);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
