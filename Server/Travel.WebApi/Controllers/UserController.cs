using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Travel.WebApi.ClientSide.Authentication.Helpers;
using Travel.WebApi.ClientSide.Authentication.Models;
using Travel.WebApi.ClientSide.Models;
using Travel.WebApi.Services;

namespace Travel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var response = _userService.Login(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                // create user
                _userService.Register(model);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [ClientSide.Authentication.Helpers.Authorize]
        [HttpGet]
        public List<UserClient> GetAll()
        {
            var a = HttpContext.Request;
            return _userService.GetAll();
        }
    }
}
