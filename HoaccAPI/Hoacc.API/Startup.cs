using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using HoaccAPI.BindingModels;
using HoaccAPI.Middlewares;
using HoaccAPI.Validation;
using HoaccCommon.Goals;
using HoaccCommon.Notification;
using HoaccCommon.Operations;
using HoaccDataSql;
using HoaccDataSql.Goals;
using HoaccDataSql.Migrations;
using HoaccDataSql.Notification;
using HoaccDataSql.Operations;
using HoaccDataSql.User;
using HoaccIServices.Goals;
using HoaccIServices.Notification;
using HoaccIServices.Operations;
using HoaccIServices.User;
using HoaccServices.Goals;
using HoaccServices.Notification;
using HoaccServices.Operations;
using HoaccServices.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HoaccAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(setup =>
        {
            setup.AddPolicy("default", options => { options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); });
        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
                };
            });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo {Title = "Hoacc", Version = "v1"});
            // Add support for JWT authentication in Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
        
        services.AddDbContext<HoaccDbContext>(options => options
            .UseMySQL(Configuration.GetConnectionString("HoaccDbContext")));
        services.AddTransient<DatabaseSeed>();
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
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
        services.AddScoped<IUserRepositoryDTO, UserRepository>();
        services.AddScoped<IOperationsService, OperationsService>();
        services.AddScoped<IOperationsRepositoryDTO, OperationsRepository>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationRepositoryDTO, NotificationRepository>();
        services.AddScoped<IGoalsService, GoalsService>();
        services.AddScoped<IGoalsRepositoryDTO, GoalsRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hoacc"); });
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
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}