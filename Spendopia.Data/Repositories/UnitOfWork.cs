using Spendopia.Data.Interfaces;
using Spendopia.Models;

namespace Spendopia.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new Repository<Category>(_context);
            Transactions = new TransactionRepository(_context);
        }

        public IRepository<Category> Categories { get; private set; }

        public ITransactionRepository Transactions { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
