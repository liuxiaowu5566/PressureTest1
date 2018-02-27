using DemoService.Services.Implements.Json;
using DemoService.Services.Implements.Ninety;
using DemoService.Services.Implements.NinetyAndJson;
using DemoService.Services.Implements.Vertical;
using DemoService.Services.Implements.Zero;
using DemoService.Services.Implements.ZeroJson;
using DemoService.Services.Implements.ZeroX;
using DemoService.Services.Interface.Json;
using DemoService.Services.Interface.Ninety;
using DemoService.Services.Interface.NinetyAndJson;
using DemoService.Services.Interface.Vertical;
using DemoService.Services.Interface.Zero;
using DemoService.Services.Interface.ZeroJson;
using DemoService.Services.Interface.ZeroX;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PZhFrame.Data.DataService;

namespace DemoServeiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["RedisConfig:Host"];
            });
            
            services.AddTransient<IZeroService, ZeroService>();
            services.AddTransient<IZeroXService, ZeroXService>();
            services.AddTransient<IVerticalService, VerticalService>();
            services.AddTransient<IJsonDataService, JsonDataService>();
            services.AddTransient<INineJsonService, NineJsonService>();
            services.AddTransient<IZeroJsonService, ZeroJsonService>();
            services.AddTransient<INinetyService, NinetyService>();
            services.AddTransient<INinetyAndJsonService, NinetyAndJsonService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
