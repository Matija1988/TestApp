using Microsoft.EntityFrameworkCore;
using Project.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Data
{
    public class ApplicationDBContext : DbContext, IDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<VehicleMake> VehicleMakers { get; set; }

        public DbSet<VehicleModel> VehicleModels { get; set; }

    }
   
}
