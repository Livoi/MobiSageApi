using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobiSageApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobiSageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IConfiguration _config;
        IStudentRepository _oStudentRepository = null;
        ICustomersRepository _oCustomerRepository = null;

        public StudentController(IConfiguration config, IStudentRepository oStudentRepository, ICustomersRepository oCustomerRepository)
        {            _config = config;
            _oStudentRepository = oStudentRepository;
            _oCustomerRepository = oCustomerRepository;
        }
        [HttpGet]
        [Route("Gets1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Gets1()
        {
            try
            {
                var list = await _oStudentRepository.Gets1();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Gets2")]
        public async Task<IActionResult> Gets2()
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
    }
}
