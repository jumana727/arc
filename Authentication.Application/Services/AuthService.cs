using AutoMapper;
using Arc.Common.Models;
using Authentication.Application.DTOs;
using Authentication.Application.Interfaces;
using Authentication.Domain.Entities;
using Authentication.Domain.Interfaces;

namespace Authentication.Application.Services
{

    /// <summary>
    /// Implementation & Internal logic for Auth Service.
    /// </summary>
    public class AuthService : IAuthService
    {

        #region Declaration

        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor for initialize the object of "AuthService".
        /// </summary>
        /// <param name="_authRepository"></param>
        /// <param name="_mapper"></param>
        public AuthService(IAuthRepository _authRepository, IMapper _mapper)
        {
            authRepository = _authRepository;
            mapper = _mapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use for verify "User".
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public async Task<DataModel<AuthResponse>> VerifyUser(AuthRequest credential)
        {
            DataModel<AuthResponse> result = new DataModel<AuthResponse>() { Data = new AuthResponse() };
            AuthSchema Authschema = mapper.Map<AuthSchema>(credential);
            DataModel<AuthSchema> response = await authRepository.VerifyUser(Authschema);
            AuthResponse data = mapper.Map<AuthResponse>(Authschema);
            result.Data = data;
            result.Status = response.Status;
            result.Message = response.Message;
            return result;
        }

        #endregion

    }

}