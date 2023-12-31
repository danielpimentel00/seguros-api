﻿using Microsoft.AspNetCore.Mvc;
using seguros_restapi.Services;

namespace seguros_restapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService customerService = new();
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await customerService.GetCustomerById(id);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
