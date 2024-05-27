using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.MVC.Models;
using Project.Service.Model;
using Project.Service.Repository;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller, IController<VehicleMakeDTOInsert>
    {
        private readonly IVehicleService
            <VehicleMake, 
            VehicleMakeDTORead, 
            VehicleMakeDTOInsert, 
            VehicleMakeDTOReadWithoutID> _vehicleMakeService;
        public VehicleMakeController(IVehicleService
            <VehicleMake,
            VehicleMakeDTORead,
            VehicleMakeDTOInsert,
            VehicleMakeDTOReadWithoutID> vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }
        /// <summary>
        /// Dohvacanje svih VehicleMake, strnicenje, filtriranje, sortiranje
        /// Get all vehicleMake, pagination, filter, sort
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>

        public async Task<IActionResult> Index(string condition, string sortOrder, int pageNumber)
        {
            var response = await _vehicleMakeService.GetAll();

            int pageSize = 10;

            if (response.Success)
            {
                var  entityList = response.Data;

                if(entityList is null)
                {
                    return NotFound();  
                }
                
                if (condition is not null)
                {
                    entityList = await Filter(condition, entityList);
                    return View(await PaginatedListView<VehicleMakeDTORead>.Paginate(entityList, pageNumber, pageSize));

                }

                entityList = await SortByAbrv(sortOrder, entityList);

                return View(await PaginatedListView<VehicleMakeDTORead>.Paginate(entityList, pageNumber, pageSize));
            }


            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.Message);

        }

        /// <summary>
        /// View init for CreateEntity
        /// </summary>
        /// <returns></returns>

        public IActionResult CreateEntity()
        {
            return View();
        }

        /// <summary>
        /// Post data 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> CreateEntity(VehicleMakeDTOInsert dto)
        {
           var response = await _vehicleMakeService.CreateEntity(dto);

            if(response.Success) 
            {
                return RedirectToAction("Index");
            }

            return StatusCode(StatusCodes.Status400BadRequest, response.Message);

        }

        public async Task<IActionResult> DeleteEntity(int id)
        {
           var response = await _vehicleMakeService.DeleteEntity(id);

            if(response.Success)
            { 
                return RedirectToAction("Index"); 
            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.Message);

        }

     

        [HttpGet]
        public async Task<IActionResult> UpdateEntity(int id)
        {
            var response = await _vehicleMakeService.GetSingleEntity(id);

            if( response.Success )
            {
                return View(response.Data);
            }

            return RedirectToAction("Index");   
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEntity(VehicleMakeDTOInsert dto, int id)
        {
            var response = await _vehicleMakeService.UpdateEntity(dto, id);

            if(response.Success)
            {
                return RedirectToAction("Index");
            }

            return View(StatusCode(StatusCodes.Status500InternalServerError, response.Message));
        }

        private async Task<List<VehicleMakeDTORead>> SortByAbrv(string sortOrder, List<VehicleMakeDTORead> entityList)
        {
            ViewData["AbrvSortParam"] = string.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";

            switch (sortOrder)
            {
                case "abrv_desc":
                    entityList = entityList.OrderByDescending(e => e.Abrv).ToList();
                    break;

                default:
                    entityList = entityList.OrderBy(e => e.Abrv).ToList();
                    break;

            }

            return entityList;
        }

        private async Task<List<VehicleMakeDTORead>> Filter(string condition, List<VehicleMakeDTORead> entityList)
        {
            condition = condition.ToLower();

            return entityList = entityList.Where
                (n => n.Abrv.ToLower().Contains(condition)
                || n.Name.ToLower().Contains(condition))
                .OrderBy(n => n.Abrv)
                .ToList();
        }

    }
}
