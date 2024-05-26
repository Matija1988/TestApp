using AutoMapper;
using Ninject.Modules;
using Project.Service.Data;
using Project.Service.Model;
using System.Globalization;

namespace Project.MVC
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {

            Bind<ApplicationDBContext>().ToSelf().InTransientScope();

            
            Bind<IDBContext>().To<ApplicationDBContext>().InTransientScope();
            //Bind<IMapping>().To<MapperConfiguration>();
            ////   Bind<MapperConfiguration>().ToSelf();

            //Bind<IVehicleService<VehicleMake, VehicleMakeDTORead, VehicleMakeDTOInsert, VehicleMakeDTOReadWithoutID>>()
            //    .To
            //    <VehicleMakeService>().InTransientScope();


            //Bind<IVehicleService
            //    <VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert, VehicleModelDTOReadWithoutID>>()
            //    .To
            //    <VehicleModelService>();
            //Bind<VehicleModelService>().ToSelf();

            //Bind<IController<VehicleMakeDTOInsert>>().To<VehicleMakeController>();
            //Bind<VehicleMakeController>().ToSelf();
        }
    }
}
