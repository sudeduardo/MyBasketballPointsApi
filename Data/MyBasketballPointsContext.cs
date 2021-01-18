using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBasketballPointsApi.Models;

namespace MyBasketballPointsApi.Data
{
    public class MyBasketballPointsContext : DbContext
    {
        public MyBasketballPointsContext (DbContextOptions<MyBasketballPointsContext> options)
            : base(options)
        {
        }

        public DbSet<MyBasketballPointsApi.Models.Game> Game { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){
          
        }
    }
}
