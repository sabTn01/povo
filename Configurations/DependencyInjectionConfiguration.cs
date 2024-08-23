using POVO.Backend.Domains.Polls;
using POVO.Backend.Infrastructure.Repositories.Polls;
using POVO.Backend.Services.Polls;

namespace POVO.Backend.Configurations
{
    public static partial class DependencyInjectionConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            ConfigureInfrastructureServices(services);
            ConfigureRepositoriesServices(services);
            ConfigureApplicationServices(services);
        }

        private static void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IPollService, PollService>();
        }
        private static void ConfigureRepositoriesServices(IServiceCollection services)
        {
            services.AddScoped<IPollRepository, PollSqlRepository>();
        }

        private static void ConfigureInfrastructureServices(IServiceCollection services)
        {
            
        }
   
    }
}
