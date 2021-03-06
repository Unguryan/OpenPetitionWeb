using Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUI.DB;
using WebUI.Hubs;
using WebUI.Middleware;
using WebUI.Services;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Web_Context>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPetitionService, PetitionService>();
            services.AddHttpContextAccessor();

            services.AddSignalR();

            services.AddControllersWithViews();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<UserCookieMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Guest}/{action=Index}");

                endpoints.MapHub<NotificationHub>("/NotificationHub");
            });
        }
    }
}
