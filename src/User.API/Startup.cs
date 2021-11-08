using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Application.CommandHandlers;
using MediatR;
using User.Domain.UserAggregate;
using User.Infrastructure.Persistence.UserAggregate.Repositories;
using User.Infrastructure.Persistence.OutboxAggregate.Repositories;
using User.Domain.OutboxAggregate;
using User.Domain.Kernel;

namespace User.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User.API", Version = "v1" });
            });

            #region MediatR

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateUserCommandHandler).Assembly);

            #endregion

            #region ApplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                           });
            },
                     ServiceLifetime.Scoped);
            #endregion

            #region UnitOfWork
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            #endregion

            #region UserAggregate
            services.AddScoped<IUserRepository, UserEfRepository>();
            #endregion

            #region OutboxAggregate
            services.AddScoped<IIntegrationEventRepository, IntegrationEventEfRepository>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
