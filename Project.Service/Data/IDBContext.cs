using Microsoft.EntityFrameworkCore;
using Project.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Data
{
    public interface IDBContext
    {
        DbSet<VehicleMake> VehicleMakers { get; }

        DbSet<VehicleModel> VehicleModels { get; }
    }
}
