using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.Controllers;
using Project.Service.Mapping;
using Project.Service.Model;
using Project.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Test.ControllerTest
{
    public class VehicleMakeControllerTest
    {

        private readonly IVehicleService<VehicleMake,
            VehicleMakeDTORead,
            VehicleMakeDTOInsert,
            VehicleMakeDTOReadWithoutID> _vehicleMakerService;

        private VehicleMakeController _vehicleMakeController;

        private readonly IMapping _mapping;

        public VehicleMakeControllerTest()
        {
            //Dependencies
            _vehicleMakerService = A.Fake<IVehicleService
                <VehicleMake,
                VehicleMakeDTORead,
                VehicleMakeDTOInsert,
                VehicleMakeDTOReadWithoutID>>();

            _mapping = A.Fake<IMapping>();

            //SUT

            _vehicleMakeController = new VehicleMakeController(_vehicleMakerService);
        }

        [Fact]
        public async void VehicleMakeController_Create_ReturnsOK()
        {

            var vehicleMake = A.Fake<ServiceResponse<VehicleMake>>();

            var makerDTOInsert = A.Fake<VehicleMakeDTOInsert>();

            A.CallTo(() => _vehicleMakerService.CreateEntity(makerDTOInsert)).Returns(vehicleMake);

            var controller = new VehicleMakeController(_vehicleMakerService);

            var result = controller.CreateEntity(makerDTOInsert);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

        [Fact]

        public async void VehicleMakeController_Update_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var vehicleMake = A.Fake<ServiceResponse<VehicleMake>>();

            var makerDTOInsert = A.Fake<VehicleMakeDTOInsert>();

            A.CallTo(() => _vehicleMakerService.UpdateEntity(makerDTOInsert, id)).Returns(vehicleMake);

            var controller = new VehicleMakeController(_vehicleMakerService);

            //Act

            var result = controller.UpdateEntity(makerDTOInsert, id);

            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }


    }

}
