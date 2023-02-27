using CorePush.Apple;
using CorePush.Google;
using Microsoft.OpenApi.Models;
using MtchMKRAPI.Data.Entities;
using MtchMKRAPI.Services;

namespace MtchMKRAPI
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
            services.AddControllers();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FcmNotificationSetting>(appSettingsSection);

            // Register the swagger generator
            services.AddSwaggerGen(c => {
                c.SwaggerDoc(name: "V1", new OpenApiInfo { Title = "My API", Version = "V1" });
            });

           // services.AddScoped<INotificationService, NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable the SwaggerUI
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
