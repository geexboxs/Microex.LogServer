using System;
using Microex.Common.Mvc.Extensions;
using Microex.LogServer.Database;
using Microex.LogServer.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Microex.LogServer.Api
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
            services.AddLogging(builder =>
            {
                builder.AddFile(options =>
                {
                    options.FileName = "log-"; // The log file prefixes
                    options.LogDirectory = "c:\\iislogs\\microex-logserver"; // The directory to write the logs
                    options.FileSizeLimit = 20 * 1024 * 1024; // The maximum log file size (20MB here)
                    options.RetainedFileCountLimit = 20;
                });
                builder.SetMinimumLevel(LogLevel.Error);
            });

            services.AddScoped<ILogger>(x=>x.GetRequiredService<ILoggerFactory>().CreateLogger("default"));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Microex.LogServer", Version = "v1" });
            });

            services.AddDbContext<LoggingDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            ////这里原来使用的inmemoryclient，修改为数据库存储
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddAspNetIdentity<AccountEntity>()
            //    .AddClientStore<ClientService>()
            //    .AddResourceStore<ResourceService>()
            //    .AddInMemoryIdentityResources(new[] { new IdentityResources.OpenId() })
            //    .AddProfileService<ProfileService>()
            //    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            //    .AddJwtBearerClientAuthentication();

            services.AddScoped<LoggingService>();

            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(10);
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAutoMigrate<LoggingDbContext>();

            //app.UseIdentityServer();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //options.RoutePrefix = "swagger/ui";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Microex.ApiGateway.V1");
            });

            app.UseMvc(route =>
            {
                route.MapRoute("default", "v1/{controller}/{action}");
            });
        }
    }
}
