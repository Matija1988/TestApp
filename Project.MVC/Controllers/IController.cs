using Microsoft.AspNetCore.Mvc;

namespace Project.MVC.Controllers
{
    public interface IController<TDI>
    {
        Task<IActionResult> Index(string condition, string sortOrder, int pageNumber);

        Task<IActionResult> CreateEntity(TDI dto);

        Task<IActionResult> UpdateEntity(TDI dto, int id);

        Task<IActionResult> DeleteEntity(int id);

    }
}
