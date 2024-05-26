using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private readonly IVehicleService
            <VehicleMake,
            VehicleMakeDTORead,
            VehicleMakeDTOInsert,
            VehicleMakeDTOReadWithoutID> _vehicleMakeService;
        public VehicleModelController(IVehicleService
            
            <VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID> vehicleModelService,
            
            IVehicleService
            <VehicleMake,
            VehicleMakeDTORead,
            VehicleMakeDTOInsert,
            VehicleMakeDTOReadWithoutID> vehicleMakeService)
        {
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
        }


        public async Task<IActionResult> CreateEntity()
        {
            await PopulateDropdown();
            return  View();
        }

      

        [HttpPost]
        public async Task<IActionResult> CreateEntity(VehicleModelDTOInsert dto)
        {
            var response = await _vehicleModelService.CreateEntity(dto);

            if(response.Success)
            {
                return RedirectToAction("Index");   
            }

            return StatusCode(StatusCodes.Status400BadRequest, response.Message);

        }

        public async Task<IActionResult> DeleteEntity(int id)
        {
            var response = await _vehicleModelService.DeleteEntity(id);

            if(response.Success)
            {
                return RedirectToAction("Index");
            }

            return BadRequest(response.Message);

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

        [HttpGet]
        public async Task<IActionResult> UpdateEntity(int id)
        {
            var response = await _vehicleModelService.GetSingleEntity(id);

            if (!response.Success)
            {
                return RedirectToAction("Index");   
            }

            
            int makeId = response.Data.Maker;

            var entityFromDb = new VehicleModelDTOInsert(
                response.Data.Name, 
                response.Data.Abrv, 
                response.Data.Maker);
            
            await PopulateDropdown(makeId);
            return View(entityFromDb);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateEntity(VehicleModelDTOInsert dto, int id)
        {
            var response = await _vehicleModelService.UpdateEntity(dto, id);

            if(response.Success)
            {
                return RedirectToAction("Index");
            }

            return View();

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

        private async Task PopulateDropdown()
        {
            var response = await _vehicleMakeService.GetAll();

            ViewBag.VehicleMakerList = new SelectList(response.Data, "Id", "Abrv");

        }

        private async Task PopulateDropdown(int makeId)
        {
            var response = await _vehicleMakeService.GetAll();

            ViewBag.VehicleMakerList = new SelectList(response.Data, "Id", "Abrv");
        }


    }
}
