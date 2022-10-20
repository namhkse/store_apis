namespace store_api.Models
{
    public class JwtResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredTime { get; set; }
    }
}