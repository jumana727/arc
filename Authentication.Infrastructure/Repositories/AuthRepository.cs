using System.Text.Json;
using Arc.Common.Enums;
using Arc.Common.Models;
using Authentication.Domain.Entities;
using Authentication.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Authentication.Infrastructure.Repositories
{

    /// <summary>
    /// Implemntation & Internal logic for Authorization repository.
    /// </summary>
    public class AuthRepository : IAuthRepository
    {

        #region Declaration

        private readonly HttpClient _httpClient;
        private readonly string _tokenEndpoint;
        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize object of "AuthRepository".
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public AuthRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _tokenEndpoint = configuration.GetSection("Authentication").GetValue<string>("Token")
                ?? throw new Exception("Cannot find Authentication Token Endpoint");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use to verify "User" information.
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public async Task<DataModel<AuthSchema>> VerifyUser(AuthSchema credential)
        {
            DataModel<AuthSchema> result = new DataModel<AuthSchema>() { Data = new AuthSchema() };

            try
            {
                var formData = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("grant_type", credential.GrantType),
            new KeyValuePair<string, string>("client_id", credential.ClientId),
            new KeyValuePair<string, string>("username", credential.UserName),
            new KeyValuePair<string, string>("password", credential.Password)
        });
                var response = await _httpClient.PostAsync(_tokenEndpoint, formData);
                string content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    using JsonDocument doc = JsonDocument.Parse(content);
                    JsonElement root = doc.RootElement;
                    string accessToken = root.GetProperty("access_token").GetString() ?? string.Empty;
                    credential.Token = accessToken;
                    result.Status = HttpStatusCode.OK;
                }
                else
                {
                    result.Status = HttpStatusCode.Unauthorized;
                    result.Message = "Username or Password incorrect.";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
            }

            result.Data = credential;
            return result;
        }

        #endregion

    }

}