using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gateway.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gateway.api.Controllers
{
    [ApiController]
    [Route("gateway")]
    public class ProxyController : ControllerBase
    {
        private readonly DataService _dataService;

        public ProxyController(DataService dataService) {
            _dataService = dataService;
        }

        [HttpGet("data")]
        public async Task<IActionResult> GetDataAsync() {
            var result = await _dataService.GetDataAsync();

            return Ok(result);
        }
    }
}

