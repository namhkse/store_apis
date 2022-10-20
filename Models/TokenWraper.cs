namespace store_api.Models
{
    public class TokenWraper
    {
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}