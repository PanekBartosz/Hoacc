﻿using FluentValidation;
using FluentValidation.AspNetCore;
using HoaccAPI.BindingModels;
using HoaccAPI.Middlewares;
using HoaccAPI.Validation;
using HoaccDataSql;
using HoaccDataSql.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HoaccDataSql.User;
using HoaccIData.User;
using HoaccIServices.User;
using HoaccIServices.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CreateUser = HoaccAPI.BindingModels.CreateUser;
using UpdateUserPassword = HoaccAPI.BindingModels.UpdateUserPassword;

namespace HoaccAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors((setup) =>
                {
                    setup.AddPolicy("default", (options) =>
                    {
                        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                    });
                });
            services.AddDbContext<HoaccDbContext>(options => options
                .UseMySQL(Configuration.GetConnectionString("HoaccDbContext")));
            services.AddTransient<DatabaseSeed>();
            services.AddControllers()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation();
            services.AddTransient<IValidator<UpdateUserPassword>, UpdateUserPasswordValidator>();
            services.AddTransient<IValidator<CreateUser>, CreateUserValidator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddApiVersioning( o =>
            {
                o.ReportApiVersions = true;
                o.UseApiBehavior = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HoaccDbContext>();
                var databaseSeed = serviceScope.ServiceProvider.GetRequiredService<DatabaseSeed>();
                
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                databaseSeed.Seed();
            }

            app.UseCors("default");
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }
    }
}