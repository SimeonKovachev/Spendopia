using Microsoft.EntityFrameworkCore;
using Spendopia.Data.Interfaces;
using Spendopia.Models;

namespace Spendopia.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Transaction>> GetAllWithCategoriesAsync()
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdWithCategoryAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .SingleOrDefaultAsync(t => t.TransactionId == id);
        }
    }
}
