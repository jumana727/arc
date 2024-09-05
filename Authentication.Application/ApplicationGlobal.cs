using Microsoft.Extensions.DependencyInjection;
using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain.Interfaces;
using Authentication.Infrastructure.Repositories;
using Authentication.Application.Mapper;

namespace Authentication.Application
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
        public static void DependencyInject(IServiceCollection services)
        {
            InjectMapper(services);
            InjectRepository(services);
            InjectService(services);
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
            services.AddScoped<IAuthRepository, AuthRepository>();
        }

        /// <summary>
        /// Service injection mechanism into environment.
        /// </summary>
        /// <param name="services"></param>
        private static void InjectService(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }

        #endregion

        #endregion

    }

}