using System.Linq;

namespace SportsStore.Models
{
    public class EFRepository : IStoreRepository
    {
        private readonly StoreDbContext _context;

        public EFRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
