using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Mapping;
using Project.Service.Model;
using Project.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Test.RepositoryTest
{
    public class VehicleModelServiceTest
    {
        private readonly IMapping _mapping;

        [Fact]
        public async void VehicleModel_Service_SearchByNameOrAbrv_ReturnsTaskServiceResponseListVehicleModelDTORead()
        {
            string abrv = "EVO 8";

            var dbContext = await GetDbContext();

            var mapper = _mapping;

            var vehicleModelRepo = new VehicleModelService(dbContext, mapper);

            var result = vehicleModelRepo.SearchByNameOrAbrv(abrv);

            if (result is null)
            {
                result.Should().BeNull();
            }

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ServiceResponse<List<VehicleModelDTORead>>>));

        }

        [Fact]

        public async void VehicleModel_Service_GetSingleEntity_ReturnsTaskServiceResponseVehicleModelDTORead()
        {
            int id = 2;

            var dbContext = await GetDbContext();

            var mapper = _mapping;

            var vehicleModelRepo = new VehicleModelService(dbContext, mapper);

            var result = vehicleModelRepo.GetSingleEntity(id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ServiceResponse<VehicleModelDTOReadWithoutID>>));
        }

        [Fact]
        public async void VehicleModel_Service_CreateEntity_ReturnsServiceResponseVehicleModel()
        {

            string name = "EVO Revolution 8";
            string abrv = "EVO 8";
            int makeID = 2;

            var modelDTOInsert = new VehicleModelDTOInsert(name, abrv, makeID);
            var dbContext = await GetDbContext();
            var mapper = _mapping;
            var vehicleModelRepo = new VehicleModelService(dbContext, mapper);

            var result = vehicleModelRepo.CreateEntity(modelDTOInsert);

            result.Should().NotBeNull();
        }

        [Fact]

        public async void VehicleModel_Service_UpdateEntity_ReturnsServiceResponseVehicleModel()
        {
            int id = 1;
            string name = "EVO Revolution 8";
            string abrv = "EVO 8";
            int makeID = 2;

            var modelDTOInsert = new VehicleModelDTOInsert(name, abrv, makeID);
            var dbContext = await GetDbContext();
            var mapper = _mapping;

            var vehicleModelRepo = new VehicleModelService(dbContext, mapper);

            var result = await vehicleModelRepo.UpdateEntity(modelDTOInsert, id);

            result.Should().NotBeNull();

        }


        private async Task<ApplicationDBContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDBContext(options);

            databaseContext.Database.EnsureCreated();

            if (await databaseContext.VehicleModels.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.VehicleModels.Add(
                        new VehicleModel()
                        {
                            Id = i,
                            Name = "EVO Revolution 8",
                            Abrv = "EVO 8",
                            MakeId = i

                        });

                    await databaseContext.SaveChangesAsync();

                };



            }

            return databaseContext;

        }
    }
}

