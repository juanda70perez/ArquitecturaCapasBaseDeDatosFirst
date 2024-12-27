namespace WebHost.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllersWithViews();
            // Otros servicios que quieras configurar pueden ir aquí.
        }
    }
}
