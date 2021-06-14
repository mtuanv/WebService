using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Travel.WebApi.ClientSide.Authentication.Helpers;
using Travel.WebApi.ClientSide.Authentication.Models;
using Travel.WebApi.ClientSide.Common;
using Travel.WebApi.ClientSide.Models;
using Travel.WebApi.DataAccess.Extensions;
using Travel.WebApi.DataAccess.Models;

namespace Travel.WebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Login(AuthenticateRequest model);
        List<UserClient> GetAll();
        UserClient GetById(int Id);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<Users> _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IRepository<Users> userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Login(AuthenticateRequest model)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == model.Username && x.Password == model.Password).SingleOrDefault();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public List<UserClient> GetAll()
        {
            var ListUser = _userRepository.GetAll().ToList();
            if (ListUser.Count == 0)
                throw new Exception("There is no user");
            var result = ListUser.Select(x => new UserClient
            {
                Id = x.Id,
                UserName = x.Username,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Type = (AccountType)x.AccountType,
            }).ToList();
            return result;
        }

        public UserClient GetById(int Id)
        {
            UserClient model = new UserClient();
            Users user = _userRepository.Get(Id);
            if (user == null)
                throw new Exception("User not found");
            else
            {
                model.Id = user.Id;
                model.UserName = user.Username;
                model.Email = user.Email;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Type = (AccountType)user.AccountType;
            }
            return model;
        }

        // helper methods

        private string generateJwtToken(Users user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
