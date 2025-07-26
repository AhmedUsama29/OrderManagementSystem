using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Identity
{
    public class OrderManagementIdentityDbContext(DbContextOptions<OrderManagementIdentityDbContext> options) : IdentityDbContext(options)
    {
    }
}
