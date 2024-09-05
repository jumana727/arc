using Microsoft.Extensions.DependencyInjection;
using VPM.Application.Interfaces;
using VPM.Application.Services;

namespace VPM.Application
{

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
            InjectService(services);

            return services;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Service injection mechanism into environment.
        /// </summary>
        /// <param name="services"></param>
        private static void InjectService(IServiceCollection services)
        {
            services.AddScoped<IStreamService, StreamService>();
        }

        #endregion

        #endregion

    }

}