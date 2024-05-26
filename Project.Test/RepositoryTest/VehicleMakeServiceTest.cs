using AutoMapper;
using Project.Service.Mapping;
using Project.Service.Repository;
using Project.Service.Data;
using Project.Service.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using FakeItEasy;

namespace Project.Test.RepositoryTest
{
    public class VehicleMakeServiceTest
    {
        private readonly IMapping _mapping;

        private readonly IVehicleService
            <VehicleMake, 
            VehicleMakeDTORead, 
            VehicleMakeDTOInsert, VehicleMakeDTOReadWithoutID> 
            _vehicleMakeService;

        public VehicleMakeServiceTest(IVehicleService
            <VehicleMake,
            VehicleMakeDTORead,
            VehicleMakeDTOInsert, VehicleMakeDTOReadWithoutID>
            vehicleMakeService, IMapping mapping)
        {
            _mapping = mapping;
            _vehicleMakeService = vehicleMakeService;
        }

        [Fact]

        public async void VehicleMaker_Service_SeearchByNameOrAbrv_ReturnsTaskServiceResponseListVehicleMakeDTORead()
        {
            var name = "Mitsubishi";

            var dbContext = await GetDbContext();

            var mapper = _mapping;

            var vehicleMakerRepo = new VehicleMakeService(dbContext, mapper);

            var result = vehicleMakerRepo.SearchByNameOrAbrv(name);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ServiceResponse<List<VehicleMakeDTORead>>>));

        }

        [Fact]
        public async void VehicleMaker_Service_GetSingleEntity_ReturnsTaskServiceResponseVehicleMakeDTORead()
        {
            int id = 1;

            var dbContext = await GetDbContext();
            var mapper = _mapping;
            var vehicleMakerRepo = new VehicleMakeService(dbContext, mapper);

            var result = vehicleMakerRepo.GetSingleEntity(id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ServiceResponse<VehicleMakeDTOReadWithoutID>>));

        }

        [Fact]

        public async void VehicleMaker_Service_CreateEntity_ReturnsServiceResponseVehicleMake()
        {

            var makerDTOInsert = new VehicleMakeDTOInsert("Bayerische Motoren Werke GmbH", "BMW");
            var dbContext = await GetDbContext();
            var mapper = _mapping;
            var vehicleMakeRepo = new VehicleMakeService(dbContext, mapper);

            var result = await vehicleMakeRepo.CreateEntity(makerDTOInsert);

            result.Should().NotBeNull();


        }

        [Fact]

        public async void VehicleMaker_Service_UpdateEntity_ReturnServiceResponseVehicle()
        {
            int id = 1;
            var makerDTOInsert = new VehicleMakeDTOInsert("Mercedes-Benz", "MBZ");
            var dbContext = await GetDbContext();
            var mapper = _mapping;
            var vehicleMakeRepo = new VehicleMakeService(dbContext, mapper);

            var result = await vehicleMakeRepo.UpdateEntity(makerDTOInsert, id);

            result.Should().NotBeNull();
        }

        private async Task<ApplicationDBContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>() 
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDBContext(options);

            databaseContext.Database.EnsureCreated();

            if (await databaseContext.VehicleMakers.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.VehicleMakers.Add(
                        new VehicleMake()
                        {
                            Id = i,
                            Name = "Mitsuhishi Motor Corporation",
                            Abrv = "Mitsubishi"

                        });

                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;

        }

    }
}

