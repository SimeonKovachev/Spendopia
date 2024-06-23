using Spendopia.Models;

namespace Spendopia.Data.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllWithCategoriesAsync();

        Task<Transaction?> GetByIdWithCategoryAsync(int id);
    }
}
