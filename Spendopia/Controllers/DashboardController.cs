using Microsoft.AspNetCore.Mvc;
using Spendopia.Services.Interfaces;

namespace Spendopia.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;

        public DashboardController(ITransactionService transactionService, ICategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            var selectedTransactions = await _transactionService.GetAllTransactionsAsync();

            int TotalIncome = selectedTransactions
                .Where(t => t.Category.Type == "Income")
                .Sum(t => t.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");

            int TotalExpense = selectedTransactions
                .Where(t => t.Category.Type == "Expense")
                .Sum(t => t.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = Balance.ToString("C0");

            var donutChartData = selectedTransactions
                .Where(t => t.Category.Type == "Expense")
                .GroupBy(t => t.Category.CategoryId)
                .Select(g => new
                {
                    categoryTitleWithIcon = g.First().Category.Icon + " " + g.First().Category.Title,
                    amount = g.Sum(t => t.Amount),
                    formattedAmount = g.Sum(t => t.Amount).ToString("C0")
                })
                .OrderByDescending(g => g.amount)
                .ToList();
            ViewBag.DonutChartData = donutChartData;

            var incomeSummary = selectedTransactions
                .Where(t => t.Category.Type == "Income")
                .GroupBy(t => t.Date)
                .Select(g => new SplineChartData
                {
                    day = g.First().Date.ToString("dd-MM"),
                    income = g.Sum(t => t.Amount)
                })
                .ToList();

            var expenseSummary = selectedTransactions
                .Where(t => t.Category.Type == "Expense")
                .GroupBy(t => t.Date)
                .Select(g => new SplineChartData
                {
                    day = g.First().Date.ToString("dd-MM"),
                    expense = g.Sum(t => t.Amount)
                })
                .ToList();

            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in incomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in expenseSummary on day equals expense.day into dayExpenseJoined
                                      from expense in dayExpenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };

            ViewBag.RecentTransactions = selectedTransactions
                .OrderByDescending(t => t.Date)
                .Take(5)
                .ToList();

            return View();
        }
    }

    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;
    }
}
