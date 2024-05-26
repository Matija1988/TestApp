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


        [HttpGet]
        public async Task<IActionResult> Index(string condition, string sortOrder, int pageNumber)
        {
            var response = await _vehicleMakeService.GetAll();

            var entityList = response.Data;


            if (response.Success)
            {
                
                if (condition is not null && condition.Length > 1)
                {
                    entityList = await Filter(condition, entityList);
                }

                entityList = await SortByAbrv(sortOrder, entityList);

                int pageSize = 10;

                return View(await PaginatedListView<VehicleMakeDTORead>.Paginate(entityList, pageNumber, pageSize));
            }


            return StatusCode(StatusCodes.Status503ServiceUnavailable, response.Message);


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



        public IActionResult CreateEntity()
        {
            return View();
        }

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

        public Task<IActionResult> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _vehicleMakeService.GetAll();

            if(response.Success)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Message);

        }

        public Task<IActionResult> GetPagination(int page, int byPage)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        
        public Task<IActionResult> SearchByNameOrAbrv(string condition)
        {
            throw new NotImplementedException();
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

        [HttpPut]
        public Task<IActionResult> UpdateEntity(VehicleMakeDTOInsert dto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
