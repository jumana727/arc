using Configuration.Application.Interfaces;
using Configuration.Application.Mapper;
using Configuration.Application.Services;
using Configuration.Domain.Interfaces;
using Configuration.Infrastructure;
using Configuration.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration.Application
{

    /// <summary>
    /// Global functionalities and logics for Application layer.
    /// </summary>
    public static class ApplicationGlobal
    {

        #region Methods

        #region Public Methods

        /// <summary>
        /// Service injection mechanism into environment.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection DependencyInject(this IServiceCollection services, string connectionString)
        {
            InfraCommon.RegisterDbContext(services, connectionString);
            InjectMapper(services);
            InjectRepository(services);
            InjectService(services);

            return services;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Mapper injection mechanism into environment.
        /// </summary>
        /// <param name="services"></param>
        private static void InjectMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));
        }

        /// <summary>
        /// Repository injection mechanism into environment.
        /// </summary>
        /// <param name="services"></param>
        private static void InjectRepository(IServiceCollection services)
        {
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
        }

        /// <summary>
        /// Service injection mechanism into environment.
        /// </summary>
        /// <param name="services"></param>
        private static void InjectService(IServiceCollection services)
        {
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IComponentService, ComponentService>();
        }

        #endregion

        #endregion

    }

}