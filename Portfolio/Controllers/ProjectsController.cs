using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Data.DataAccess;
using Portfolio.Models;
using Portfolio.Models.ViewModels;

namespace Portfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IRepositoryWrapper repositoryWrapper;
        public ProjectsController(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 6;
            var options = new QueryOptions<Project>
            {
                PageNumber = page,
                PageSize = pageSize,
                OrderBy = p => p.CreatedAt,
                OrderByDirection = "desc"
            };

            var projects = repositoryWrapper.Project.GetWithOptions(options);

            // You need to calculate total pages based on total projects count:
            var totalProjects = repositoryWrapper.Project.GetAll().Count();
            int totalPages = (int)Math.Ceiling(totalProjects / (double)pageSize);

            var paginationModel = new PaginationModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                GetPageUrl = p => Url.Action("Index", new { page = p })
            };

            ViewBag.Pagination = paginationModel;

            return View(projects);
        }
        public IActionResult Details(int id)
        {
            var project = repositoryWrapper.Project.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

    }
}
