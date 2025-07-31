namespace Portfolio.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _context;
        private IProjectRepository _project;
        private IContactRepository _contact;

        public RepositoryWrapper(AppDbContext context)
        {
            _context = context;
        }

        //public IProjectRepository Project => _project ??= new ProjectRepository(_context);
        //public IContactRepository Contact => _contact ??= new ContactRepository(_context);

        public IProjectRepository Project
        {
            get
            {
                if(_project == null)
                {
                    _project = new ProjectRepository(_context);
                }
                return _project;
            }
        } public IContactRepository Contact
        {
            get
            {
                if(_contact == null)
                {
                    _contact = new ContactRepository(_context);
                }
                return _contact;
            }
        }


        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
