using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensibility;
using Project.MVC.Models;
using Project.Service.Model;
using Project.Service.Repository;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller, IController<VehicleModelDTOInsert>
    {
        private readonly IVehicleService
            <VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID> _vehicleModelService;
        public VehicleModelController(IVehicleService
            <VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID> vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        public Task<IActionResult> CreateEntity(VehicleModelDTOInsert dto)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Index(string condition, string sortOrder, int pageNumber)
        {
            var response = await _vehicleModelService.GetAll();

            int pageSize = 10;

            if (response.Success)
            {
                var entityList = response.Data;

                if (entityList is null)
                {
                    return NotFound();
                }

                if (condition is not null)
                {
                    entityList = await Filter(condition, entityList);
                    return View(await PaginatedListView<VehicleModelDTORead>.Paginate(entityList, pageNumber, pageSize));

                }

                entityList = await SortByAbrv(sortOrder, entityList);

                return View(await PaginatedListView<VehicleModelDTORead>.Paginate(entityList, pageNumber, pageSize));
            }


            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.Message);

        }

        public Task<IActionResult> UpdateEntity(VehicleModelDTOInsert dto, int id)
        {
            throw new NotImplementedException();
        }

        private async Task<List<VehicleModelDTORead>> SortByAbrv(string sortOrder, List<VehicleModelDTORead> entityList)
        {
            ViewData["MakerSortParam"] = string.IsNullOrEmpty(sortOrder) ? "maker_desc" : "";

            switch (sortOrder)
            {
                case "maker_desc":
                    entityList = entityList.OrderByDescending(e => e.Abrv).ToList();
                    break;

                default:
                    entityList = entityList.OrderBy(e => e.Abrv).ToList();
                    break;

            }

            return entityList;
        }
        private async Task<List<VehicleModelDTORead>> Filter(string condition, List<VehicleModelDTORead> entityList)
        {
            condition = condition.ToLower();

            return entityList = entityList.Where
                (n => n.Abrv.ToLower().Contains(condition)
                || n.Name.ToLower().Contains(condition)
                || n.Maker.ToLower().Contains(condition))
                .OrderBy(n => n.Abrv)
                .ToList();
        }



    }
}
