using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Data;
using Portfolio.Data.DataAccess;
using Portfolio.Models;
using Portfolio.Models.ViewModels;

namespace Portfolio.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private const int PageSize = 10;

        public AdminController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Manage Projects with optional search and paging
        public IActionResult ManageProjects(string search = "", int page = 1)
        {
            var options = new QueryOptions<Project>
            {
                PageNumber = page,
                PageSize = PageSize,
                OrderBy = p => p.CreatedAt,
                OrderByDirection = "desc"
            };

            IQueryable<Project> query = _repo.Project.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Title.Contains(search)
                                      || p.Description.Contains(search)
                                      || p.Technologies.Contains(search));
            }

            var totalProjects = query.Count();

            var projects = query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var paginationModel = new PaginationModel
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalProjects / (double)PageSize),
                GetPageUrl = p => Url.Action("ManageProjects", new { search, page = p })
            };

            ViewBag.Pagination = paginationModel;
            ViewBag.SearchTerm = search;

            return View(projects);
        }

        // Manage Messages with paging
        public IActionResult ManageMessages(int page = 1)
        {
            var options = new QueryOptions<ContactMessage>
            {
                PageNumber = page,
                PageSize = PageSize,
                OrderBy = m => m.SubmittedAt,
                OrderByDirection = "desc"
            };

            var totalMessages = _repo.Contact.GetAll().Count();

            var messages = _repo.Contact.GetAll()
                .OrderByDescending(m => m.SubmittedAt)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var paginationModel = new PaginationModel
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalMessages / (double)PageSize),
                GetPageUrl = p => Url.Action("ManageMessages", new { page = p })
            };

            ViewBag.Pagination = paginationModel;

            return View(messages);
        }

        public IActionResult EditProject(int id)
        {
            var project = _repo.Project.GetById(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(Project project)
        {
            if (!ModelState.IsValid)
                return View(project);

            _repo.Project.Update(project);
            await _repo.SaveAsync();

            TempData["SuccessMessage"] = "Project updated successfully!";
            return RedirectToAction("ManageProjects");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = _repo.Project.GetById(id);
            if (project != null)
            {
                _repo.Project.Delete(project);
                await _repo.SaveAsync();
                TempData["SuccessMessage"] = "Project deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = "Project not found.";
            }

            return RedirectToAction("ManageProjects");
        }
    }

}
