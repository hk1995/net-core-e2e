using FluentValidation.AspNetCore;
using LMS.Data;
using LMS.Web.Infrastructure;
using LMS.Web.Infrastructure.Filters;
using LMS.Web.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace LMS.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
            Configuration = builder.Build();

            AppSettings.SetEnviornmentalSettings(Configuration);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(DbContextTransactionFilter));
            })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                });

            //DI
            services.AddScoped<MultipartContentValidatorActionFilter>();
            services.AddMediatR(typeof(Startup));
            services.AddScoped(_ => new LmsContext(Configuration.GetConnectionString(AppSettings.ConnectionStringName)));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //make sure this is called first in the pipeline!!!
            app.UseCors(builder =>
                builder.WithOrigins(AppSettings.AppRoot)
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            var options = new JwtBearerOptions
            {
                Authority = AppSettings.Auth0Domain,
                Audience = AppSettings.Auth0Domain
            };

            app.UseJwtBearerAuthentication(options);
            app.UseClaimsTransformation(new ClaimsTransformationOptions
            {
                Transformer = new Auth0ClaimsTransformer()
            });

            app.UseMvc();
        }
    }
}
