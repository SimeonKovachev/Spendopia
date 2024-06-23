using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spendopia.Models;
using Spendopia.Services.Interfaces;

namespace Spendopia.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Actions(int id = 0)
        {
            if (id == 0)
                return View(new Category());

            var category = await _categoryService.GetCategoryByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actions(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                    await _categoryService.CreateCategoryAsync(category);
                else
                    await _categoryService.UpdateCategoryAsync(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
