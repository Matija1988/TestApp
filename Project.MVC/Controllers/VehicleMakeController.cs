using Microsoft.AspNetCore.Mvc;
using Project.Service.Model;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller, IController<VehicleMakeDTOInsert>
    {
        public VehicleMakeController()
        {
            
        }
        public Task<IActionResult> CreateEntity(VehicleMakeDTOInsert dto)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetPagination(int page, int byPage)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }

        public Task<IActionResult> SearchByNameOrAbrv(string condition)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateEntity(VehicleMakeDTOInsert dto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
