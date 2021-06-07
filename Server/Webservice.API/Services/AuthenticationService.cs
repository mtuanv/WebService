using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Webservice.API.ClientSide.Common;
using Webservice.API.ClientSide.Models;
using Webservice.API.DataAccess.Models;

namespace Webservice.API.Services
{
    public interface IAuthentication
    {
        string Authenticate(string user, string password);
        AccountClient Create(AccountClient accountClient);
        AccountClient Update(AccountClient accountClient);
        bool Delete(int account_Id);
        AccountClient GetAccount(int id);
    }
    public class AuthenticationService : IAuthentication
    {
        private readonly WebserviceContext _context;
        private readonly string key;
        public AuthenticationService(string key, WebserviceContext context)
        {
            this.key = key;
            _context = context;

        }
        public string Authenticate(string username, string password)
        {
            var user = _context.Account.SingleOrDefault(a => a.UserName == username);
            Console.WriteLine(user);
            if (user.UserName == username && user.Password == password)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescription);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                throw new Exception("Login failed");
            }
        }

        public AccountClient Create(AccountClient model)
        {
            var accoutCheck = _context.Account.Where(x => x.UserName == model.UserName && !x.DeletedDate.HasValue).FirstOrDefault();
            if(accoutCheck!= null)
                throw new Exception("Username is unavailable");
            else
            {
                var account = new Account
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Password = model.Password,

                };
                _context.Account.Add(account);
                _context.SaveChanges();
            }           
            return model;
        }

        public AccountClient Update(AccountClient model)
        {
            Account accountUpdate = _context.Account.Where(x => x.Id == model.Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if(accountUpdate == null)
                throw new Exception("Account not found");
            else
            {
                accountUpdate.UserName = model.UserName;
                accountUpdate.Password = model.Password;
                _context.SaveChanges();
            }
            return model;
        }

        public bool Delete(int Id)
        {
            Account account = null;
            account = _context.Account.Where(x => x.Id == Id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (account == null)
                throw new Exception("Account not found");
            else
            {
                _context.Account.Remove(account);
                _context.SaveChanges();
                return true;
            }
        }

        public AccountClient GetAccount(int id)
        {
            Account account = null;
            account = _context.Account.Where(x => x.Id == id && !x.DeletedDate.HasValue).FirstOrDefault();
            if (account == null)
                throw new Exception("Account not found");
            else
            {
                AccountClient accountClient = new AccountClient();
                accountClient.UserName = account.UserName;
                accountClient.Password = account.Password;
                if(account.Role == 2)
                {
                    accountClient.Role = AccountRole.GUIDER;
                }else if(account.Role == 1)
                {
                    accountClient.Role = AccountRole.TRAVELER;
                }

                return accountClient;
            }
        }
    }
}
