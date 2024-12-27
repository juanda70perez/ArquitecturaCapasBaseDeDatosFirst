using Dal.UnitOfWorks;

namespace WebHost.Configurations
{
    public static class UnitOfWorkConfiguration
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // O agregar más servicios relacionados a la unidad de trabajo si es necesario
        }
    }
}
