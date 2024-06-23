using Spendopia.Models;

namespace Spendopia.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

        Task<Transaction?> GetTransactionByIdAsync(int id);

        Task CreateTransactionAsync(Transaction transaction);

        Task UpdateTransactionAsync(Transaction transaction);

        Task DeleteTransactionAsync(int id);
    }
}
