using FBTarjetasCredito.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBTarjetasCredito
{
    public class ApplicationDBContext : DbContext //La clase heredea de DbContext
    {
        public DbSet<TarjetaCredito> TarjetaCreditoDB { get; set; } //El modelo

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base (options)
        {

        }

    }
}
