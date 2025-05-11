using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<Page?> GetByUrlAsync(string url);
        Task<bool> CheckUrl(string url);
    }
}
