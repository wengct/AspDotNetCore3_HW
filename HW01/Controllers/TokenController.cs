using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW01.Helpers;
using HW01.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtHelpers helper;
        public TokenController(JwtHelpers helper)
        {
            this.helper = helper;
        }
        // POST api/token
        [HttpPost("")]
        public IActionResult Poststring(LoginViewModel loginViewModel)
        {
            if (ValidateLogin(loginViewModel))
            {
                return Ok(new { token = helper.GenerateToken(loginViewModel.Username) });
            }
            else
            {
                return Forbid();
            }
        }

        private bool ValidateLogin(LoginViewModel loginViewMOdel)
        {
            return true;
        }
    }
}