using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.App.Databases;
using Project.App.DesignPatterns.Reponsitories;
using Project.App.Mqtt;
using Project.App.Providers;
using Project.App.Schedules;
using Project.App.Schedules.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfiguration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Add Cors Service
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    List<string> allowedOrigins = new();
                    if (Configuration.GetSection($"AllowedOrigins").Exists() && !string.IsNullOrEmpty(Configuration.GetSection($"AllowedOrigins").Get<string>()) && !Configuration.GetSection($"AllowedOrigins").Get<string>().Split(";").Any(x => x.Equals("*")))
                    {
                        allowedOrigins.AddRange(Configuration.GetSection($"AllowedOrigins").Get<string>().Split(";"));
                    }
                    else
                    {
                        allowedOrigins.Add("*");
                    }
                    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins(allowedOrigins.ToArray())
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            #endregion
            services.AddDbContextPool<MariaDBContext>(
                options => options.UseMySql(Configuration["ConnectionSetting:MariaDBSettings:ConnectionStrings"],
                ServerVersion.AutoDetect(Configuration["ConnectionSetting:MariaDBSettings:ConnectionStrings"])
            ));
            services.AddScoped<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IRedisDatabaseProvider, RedisDBContext>();
            #region Add MQTT Service
            services.AddSingleton<IMqttSingletonService, MqttSingletonService>();
            services.AddHostedService<MqttHostedService>();
            #endregion
            #region Add Module Services
            services.AddScoped<IRepositoryWrapperMariaDB, RepositoryWrapperMariaDB>();
            #endregion

            services.UseQuartz(typeof(UpdatePingStatus));

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new HttpResponseExceptionFilter());
                    options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider());
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            IScheduler scheduler = app.ApplicationServices.GetService<IScheduler>();
            QuartzServicesUtilitie.StartJob<UpdatePingStatus>(scheduler, Configuration["ScheduleSettings:UpdateLogStatus"]);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}