using Microsoft.EntityFrameworkCore;
using PN.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PN.WebAPI.DAL
{
   public class EFContext : DbContext
   {
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                               => optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=postgres;Password=123456;Database=Pcis;Pooling=true;");

      public DbSet<User> users { get; set; }
   }
}
