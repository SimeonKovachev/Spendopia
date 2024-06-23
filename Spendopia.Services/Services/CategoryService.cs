using Spendopia.Data.Interfaces;
using Spendopia.Models;
using Spendopia.Services.Interfaces;

namespace Spendopia.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _unitOfWork.Categories.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
            }
        }
    }
}
