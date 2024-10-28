using Arc.Common.Models;
using Authentication.Application.DTOs;

namespace Authentication.Application.Interfaces
{

    /// <summary>
    /// Blueprint for Auth Service.
    /// </summary>
    public interface IAuthService
    {

        #region Methods

        /// <summary>
        /// Use for verify "User".
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<DataModel<AuthResponse>> VerifyUser(AuthRequest formData);

        #endregion

    }

}