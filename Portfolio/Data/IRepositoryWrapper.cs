namespace Portfolio.Data
{
    public interface IRepositoryWrapper
    {
        IProjectRepository Project { get; }
        IContactRepository Contact { get; }
        Task SaveAsync();
    }
}
