namespace Authentication.Application.DTOs
{

    public class AuthRequest
    {

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string GrantType { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

    }

}