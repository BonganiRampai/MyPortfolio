using Portfolio.Data.DataAccess;
using Portfolio.Models;

namespace Portfolio.Data
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public IEnumerable<Project> GetLatestProjects(int count)
        {
            return _appDbContext.Projects
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .ToList();
        }

        public IEnumerable<Project> GetProjectsWithPaging(QueryOptions<Project> options)
        {
            return GetWithOptions(options);
        }

        public Project GetProjectById(int id)
        {
            return _appDbContext.Projects
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefault();
        }

        public Project GetProjectBySlug(string slug)
        {
            return _appDbContext.Projects
                .FirstOrDefault(p => p.Title.ToLower().Replace(" ", "-") == slug.ToLower());
        }


        public IEnumerable<Project> SearchProjects(string keyword)
        {
            return _appDbContext.Projects
                .Where(p =>
                    p.Title.Contains(keyword) ||
                    p.Description.Contains(keyword) ||
                    p.Technologies.Contains(keyword))
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }
    }
}
