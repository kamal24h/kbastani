using DataAccess;
using DataAccess.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Service.Contract;

namespace Common.DependencyInjection;

public class Bootstrap
{
    public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
    {
        //Data Access
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IBlogPostRepository, BlogPostRepository>();
        services.AddTransient<ITagRepository, TagRepository>();
        //services.AddTransient<IResidentRepository, ResidentRepository>();
        services.AddTransient<IProjectRepository, ProjectRepository>();

        //Services
        services.AddTransient<IBlogPostService, BlogPostService>();
        services.AddTransient<ITagService, TagService>();
        //services.AddTransient<IResidentService, ResidentService>();
        services.AddTransient<IProjectService, ProjectService>();



        // Add application services.
        //services.AddTransient<IEmailSender, AuthMessageSender>();
        //services.AddTransient<ICmsSender, AuthMessageSender>();


    }
}
