using Spendopia.Models;

namespace Spendopia.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Category> Categories { get; }

        ITransactionRepository Transactions { get; }

        Task<int> CompleteAsync();
    }
}
