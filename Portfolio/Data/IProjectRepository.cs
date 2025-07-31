using Portfolio.Data.DataAccess;
using Portfolio.Models;

namespace Portfolio.Data
{
    public interface IProjectRepository:IRepositoryBase<Project>
    {
        IEnumerable<Project> GetLatestProjects(int count);
        Project GetProjectBySlug(string slug);
        IEnumerable<Project> SearchProjects(string keyword);
        IEnumerable<Project> GetProjectsWithPaging(QueryOptions<Project> options);
        Project GetProjectById(int id);
    }
}
