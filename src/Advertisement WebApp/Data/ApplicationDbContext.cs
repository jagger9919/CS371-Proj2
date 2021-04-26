using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using Advertisement_WebApp.Models;

namespace Advertisement_WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Advertisements> Advertisements { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Moderators> Moderators { get; set; }
        public DbSet<Statuses> Statuses { get; set; }
        public DbSet<Users> advUsers { get; set; }
    }
}
