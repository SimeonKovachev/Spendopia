using Microsoft.Extensions.DependencyInjection;
using Spendopia.Data.Interfaces;
using Spendopia.Data.Repositories;
using Spendopia.Data;
using Spendopia.Models;
using Spendopia.Services.Interfaces;
using Spendopia.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace Spendopia.Services.Configurations
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Register services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
