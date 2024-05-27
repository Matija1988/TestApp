//using AutoMapper;
//using Ninject.Modules;
//using Project.Service.Data;
//using Project.Service.Model;
//using System.Globalization;

using Ninject.Modules;
using Project.Service.Data;
using Project.Service.Mapping;
using Project.Service.Model;
using Project.Service.Repository;

namespace Project.MVC
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {

            Bind<IDBContext>().To<ApplicationDBContext>().InTransientScope();

            Bind<IMapping>().To<MapperConfig>();

            Bind<IVehicleService<VehicleMake, VehicleMakeDTORead, VehicleMakeDTOInsert, VehicleMakeDTOReadWithoutID>>()
                .To
                <VehicleMakeService>();


            Bind<IVehicleService
                <VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert, VehicleModelDTOReadWithoutID>>()
                .To
                <VehicleModelService>();
            
         
        }
    }
}
