using Portfolio.Models;

namespace Portfolio.Data
{
    public class ContactRepository : RepositoryBase<ContactMessage>, IContactRepository
    {
        public ContactRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public IEnumerable<ContactMessage> GetLatestMessages(int count)
        {
            return _appDbContext.ContactMessages
                .OrderByDescending(m => m.SubmittedAt)
                .Take(count)
                .ToList();
        }
    }
}
