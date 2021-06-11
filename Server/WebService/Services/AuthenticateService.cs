﻿//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using WebService.ClientSide.Authentication;

//namespace WebService.Services
//{
//    public interface IAuthenticateService
//    {
//        ActionResult<LoginModel> Login([FromBody] LoginModel model);
//        ActionResult<RegisterModel> Register([FromBody] RegisterModel model);
//    }
//    public class AuthenticateService
//    {
//        private readonly UserManager<ApplicationUser> userManager;
//        private readonly RoleManager<IdentityRole> roleManager;
//        private readonly IConfiguration _configuration;
//        public AuthenticateService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
//        {
//            this.userManager = userManager;
//            this.roleManager = roleManager;
//            _configuration = configuration;
//        }

//        public async Task<ActionResult<LoginModel>> Login([FromBody] LoginModel model)
//        {
//            var user = await userManager.FindByNameAsync(model.Username);
//            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
//            {
//                var userRoles = await userManager.GetRolesAsync(user);
//                var authClaims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
//                };
//                foreach (var userRole in userRoles)
//                {
//                    authClaims.Add(new Claim(ClaimTypes.Role, userRole)); //add quyeefn
//                }
//                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
//                var token = new JwtSecurityToken(
//                    issuer: _configuration["JWT:ValidIssuer"],
//                    audience: _configuration["JWT:ValidAudience"],
//                    expires: DateTime.Now.AddDays(1),
//                    claims: authClaims,
//                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
//                return Ok(new
//                {
//                    token = new JwtSecurityTokenHandler().WriteToken(token),
//                    expiration = token.ValidTo
//                });
//            }
//            return Unauthorized();
//        }
//    }
//}
