using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//using HW01.Models;

namespace HW01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        public AppSettings AppSettings { get; }
        public OptionController(IOptions<AppSettings> options)
        {
            this.AppSettings = options.Value;
        }

        // GET api/option/5
        [HttpGet("{id}")]
        public ActionResult<AppSettings> GetAppSettings(int id)
        {
            return this.AppSettings;
        }
    }
}