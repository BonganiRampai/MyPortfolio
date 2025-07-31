using Portfolio.Models;

namespace Portfolio.Data
{
    public interface IContactRepository : IRepositoryBase<ContactMessage>
    {
        IEnumerable<ContactMessage> GetLatestMessages(int count);
    }
}
