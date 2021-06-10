using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SaudeMental.Api.Model;

namespace SaudeMental.Api.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<FormInfo> FormInfos { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
