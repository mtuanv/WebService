using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webservice.API;
using Webservice.API.ClientSide.Common;
using Webservice.API.ClientSide.Models;
using Webservice.API.DataAccess.Models;
using Webservice.API.Services;

namespace Webservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthentication _iAuthentication;       
        public AccountsController(IAuthentication authentication)
        {
            _iAuthentication = authentication;
        }
        //AUTHENTICATION

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate([FromBody] LoginClient login)
        {
            var token = _iAuthentication.Authenticate(login.username, login.password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        // REGISTER

        [AllowAnonymous]
        [HttpPost]
        [Route("register/guider")]
        public ActionResult<AccountClient> PostAccount(AccountClient model)
        {
            model.Role = AccountRole.GUIDER;
            return _iAuthentication.Create(model);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register/traveler")]
        public ActionResult<AccountClient> CreateUser(AccountClient model)
        {
            model.Role = AccountRole.TRAVELER;
            return _iAuthentication.Create(model);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public ActionResult<AccountClient> GetAccount(int id)
        {
            return _iAuthentication.GetAccount(id);
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public ActionResult<AccountClient> PutAccount( AccountClient model)
        {
            return _iAuthentication.Update(model);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteAccount(int id)
        {
            return _iAuthentication.Delete(id);
        }

        
    }
}
