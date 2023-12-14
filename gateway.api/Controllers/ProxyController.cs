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
        public async Task<IActionResult> GetDataAsync(double lat = 37.04149096479284, double lon = 22.112488382989756, string q = "Greece", string bandId = "4Z8W4fKeB5YxbusRsdQVPb") {
            var result = await _dataService.GetDataAsync(lat, lon, q, bandId);

            return Ok(result);
        }
    }
}

