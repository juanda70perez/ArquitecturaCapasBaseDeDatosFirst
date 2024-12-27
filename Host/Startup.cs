using WebHost.Configurations;

namespace WebHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Este método se llama en tiempo de ejecución y se usa para agregar servicios al contenedor de dependencia
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContextService(Configuration);
            services.AddRepositoryServices();
            services.AddUnitOfWorkService();
            services.AddApplicationServices();
        }

        // Este método se llama en tiempo de ejecución y se usa para configurar el pipeline de solicitudes HTTP
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}