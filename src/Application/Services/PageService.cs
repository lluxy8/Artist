using Application.Abstract;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public sealed class PageService : BaseService<Page>
    {
        private readonly IPageRepository _pageRepository;
        public PageService(IRepository<Page> repository, IPageRepository pageRepository) : base(repository)
        {
            _pageRepository = pageRepository;
        }

        public async Task<Page?> GetByUrlAsync(string url)
        {
            try
            {
                return await _pageRepository.GetByUrlAsync(url);
            }
            catch(Exception ex)
            {
                throw new Exception("Error getting page by URL", ex);
            }
        }

    }
}
