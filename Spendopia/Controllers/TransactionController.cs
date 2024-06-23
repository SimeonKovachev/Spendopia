using Microsoft.AspNetCore.Mvc;
using Spendopia.Models;
using Spendopia.Services.Interfaces;

namespace Spendopia.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;

        public TransactionController(ITransactionService transactionService, ICategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
        }

        public async Task<IActionResult> Actions(int id = 0)
        {
            PopulateCategories();
            if (id == 0)
                return View(new Transaction());

            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actions(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                    await _transactionService.CreateTransactionAsync(transaction);
                else
                    await _transactionService.UpdateTransactionAsync(transaction);

                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return NoContent();
        }

        [NonAction]
        public void PopulateCategories()
        {
            var categories = _categoryService.GetAllCategoriesAsync().Result.ToList();
            Category defaultCategory = new Category { CategoryId = 0, Title = "Choose a Category" };
            categories.Insert(0, defaultCategory);
            ViewBag.Categories = categories;
        }
    }
}
