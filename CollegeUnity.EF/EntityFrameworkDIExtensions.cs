using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.EF
{
    public static class EntityFrameworkDIExtensions
    {
        public static IServiceCollection AddCollegeUnityDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CollegeUnityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Release"));
                //options.UseSqlServer(configuration.GetConnectionString("Default"));
                //options.UseSqlServer(configuration.GetConnectionString("FaisalLocal"));
                //options.UseSqlServer(configuration.GetConnectionString("Local"));
            });

            return services;
        }

        public static IServiceCollection AddEFLayerDI(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }
    }
}
