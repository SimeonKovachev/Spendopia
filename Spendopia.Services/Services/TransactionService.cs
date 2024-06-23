using Spendopia.Data.Interfaces;
using Spendopia.Models;
using Spendopia.Services.Interfaces;

namespace Spendopia.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _unitOfWork.Transactions.GetAllWithCategoriesAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _unitOfWork.Transactions.GetByIdWithCategoryAsync(id);
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            await _unitOfWork.Transactions.AddAsync(transaction);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            await _unitOfWork.Transactions.UpdateAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (transaction != null)
            {
                await _unitOfWork.Transactions.DeleteAsync(transaction);
            }
        }
    }
}
