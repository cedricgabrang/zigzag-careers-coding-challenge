using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseInMemoryDatabase("LibraryInMemoryDb");
            });

            services.AddTransient<ILibraryDbInitializer, LibraryDbInitializer>();
        }
    }
}
