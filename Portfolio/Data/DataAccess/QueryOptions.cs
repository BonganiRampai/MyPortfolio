using System.Linq.Expressions;

namespace Portfolio.Data.DataAccess
{
    public class QueryOptions<T>
    {
        // public properties for sorting, filtering, paging and includes
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc"; // default
        public Expression<Func<T, bool>> Where { get; set; }
        public Expression<Func<T, Object>> Includes { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // read-only properties
        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasInclude => Includes != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }

}
