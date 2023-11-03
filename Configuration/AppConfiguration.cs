using HairdresserAPI.DatabaseContext;
using HairdresserAPI.UserDomain.UserManagement.Interfaces;
using HairdresserAPI.UserDomain.UserManagement.Services;
using HairdresserAPI.UserDomain.UserRepository;
using Microsoft.EntityFrameworkCore;


namespace HairdresserAPI.Configuration;

public static class AppConfiguration
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserManagementPrivateService, UserManagementPrivateService>();
        services.AddScoped<IUserManagementRepository, UserManagementRepository>();
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<AppDbContext>(options => options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 35)))
                .EnableSensitiveDataLogging(true)
                .LogTo(Console.WriteLine, LogLevel.Information));
    }

    public static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("swagger/");
                    return;
                }

                await next.Invoke();
            });
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}
