using FluentValidation;
using FluentValidation.AspNetCore;
using HoaccAPI.Middlewares;
using HoaccAPI.Validation;
using HoaccDataSql;
using HoaccDataSql.Goals;
using HoaccDataSql.Migrations;
using HoaccDataSql.Notification;
using HoaccDataSql.Operations;
using Microsoft.EntityFrameworkCore;
using HoaccDataSql.User;
using HoaccIData.Goals;
using HoaccIData.Notification;
using HoaccIData.Operations;
using HoaccIData.User;
using HoaccIServices.Goals;
using HoaccIServices.Notification;
using HoaccIServices.Operations;
using HoaccIServices.User;
using HoaccServices.Goals;
using HoaccServices.Notification;
using HoaccServices.Operations;
using HoaccServices.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CreateUser = HoaccAPI.BindingModels.CreateUser;
using UpdateUserPassword = HoaccAPI.BindingModels.UpdateUserPassword;
using CreateGoals = HoaccAPI.BindingModels.CreateGoals;
using CreateNotification = HoaccAPI.BindingModels.CreateNotification;
using CreateOperations = HoaccAPI.BindingModels.CreateOperations;
using EditGoals = HoaccAPI.BindingModels.EditGoals;
using EditNotification = HoaccAPI.BindingModels.EditNotification;
using EditOperations = HoaccAPI.BindingModels.EditOperations;

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
            services.AddTransient<IValidator<EditOperations>, EditOperationsValidator>();
            services.AddTransient<IValidator<CreateOperations>, CreateOperationsValidator>();
            services.AddTransient<IValidator<EditNotification>, EditNotificationValidator>();
            services.AddTransient<IValidator<CreateNotification>, CreateNotificationValidator>();
            services.AddTransient<IValidator<EditGoals>, EditGoalsValidator>();
            services.AddTransient<IValidator<CreateGoals>, CreateGoalsValidator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOperationsService, OperationsService>();
            services.AddScoped<IOperationsRepository, OperationsRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IGoalsService, GoalsService>();
            services.AddScoped<IGoalsRepository, GoalsRepository>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}