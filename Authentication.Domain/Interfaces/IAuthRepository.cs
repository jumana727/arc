using Arc.Common.Models;
using Authentication.Domain.Entities;

namespace Authentication.Domain.Interfaces
{

    /// <summary>
    /// Blueprint of Authorization Repository.
    /// </summary>
    public interface IAuthRepository
    {

        #region Methods

        /// <summary>
        /// Use to verify "User" information.
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        Task<DataModel<AuthSchema>> VerifyUser(AuthSchema formData);

        #endregion

    }

}