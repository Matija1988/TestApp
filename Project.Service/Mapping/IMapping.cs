using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Mapping
{
    public interface IMapping
    { 
        Task<Mapper> VehicleMakerMapReadToDTO();

        Task<Mapper> VehicleMakerUpdateFromDTO();

        Task<Mapper> VehicleModelMapReadToDTO();

        Task<Mapper> VehicleModelInsertFromDTO();

        Task<Mapper> VehicleMakerDataOfUpdatedEntity();

        Task<Mapper> VehicleModelDataOfUpdatedEntity();

    }
}
