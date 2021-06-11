using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.ClientSide.Authentication;

namespace WebService
{
    public class WebServiceDbContext : IdentityDbContext<ApplicationUser>
    {

        public WebServiceDbContext(DbContextOptions<WebServiceDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
