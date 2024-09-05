namespace Authentication.Domain.Entities
{
    
    /// <summary>
    /// Authentication Schema.
    /// </summary>
    public class AuthSchema
    {

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string GrantType { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

    }

}