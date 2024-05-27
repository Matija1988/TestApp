using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.Controllers;
using Project.Service.Mapping;
using Project.Service.Model;
using Project.Service.Repository;

namespace Project.Test.ControllerTest
{
    public class VehicleModelControllerTest
    {
        private readonly IVehicleService<
            VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID> _vehicleModelService;

        private readonly IVehicleService<VehicleMake,
           VehicleMakeDTORead,
           VehicleMakeDTOInsert,
           VehicleMakeDTOReadWithoutID> _vehicleMakerService;


        private readonly VehicleModelController _vehicleModelController;

        private readonly IMapping _mapping;


        public VehicleModelControllerTest()
        {
            _vehicleModelService = A.Fake<IVehicleService<VehicleModel,
                VehicleModelDTORead,
                VehicleModelDTOInsert,
                VehicleModelDTOReadWithoutID>>();

            _vehicleMakerService = A.Fake<IVehicleService
                <VehicleMake, 
                VehicleMakeDTORead, 
                VehicleMakeDTOInsert, 
                VehicleMakeDTOReadWithoutID >>();

            _mapping = A.Fake<IMapping>();

            _vehicleModelController = new VehicleModelController(_vehicleModelService, _vehicleMakerService);

        }

        [Fact]
        public async void VehicleModelController_Create_ReturnsOK()
        {
            var mapper = await _mapping.VehicleModelInsertFromDTO();
            var vehicleModel = A.Fake<ServiceResponse<VehicleModel>>();
            var modelDTOInsert = A.Fake<VehicleModelDTOInsert>();

            A.CallTo(() => _vehicleModelService.CreateEntity(modelDTOInsert)).Returns(vehicleModel);

            var controller = new VehicleModelController(_vehicleModelService, _vehicleMakerService);

            var result = controller.CreateEntity(modelDTOInsert);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

        [Fact]
        public async void VehicleModelController_Update_ReturnsOK()
        {
            int id = 1;
            var mapper = await _mapping.VehicleModelInsertFromDTO();
            var vehicleModel = A.Fake<ServiceResponse<VehicleModel>>();
            var modelDTOInsert = A.Fake<VehicleModelDTOInsert>();

            A.CallTo(() => _vehicleModelService.UpdateEntity(modelDTOInsert, id));

            var controller = new VehicleModelController(_vehicleModelService, _vehicleMakerService);

            var result = controller.UpdateEntity(modelDTOInsert, id);


            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

    }
}
