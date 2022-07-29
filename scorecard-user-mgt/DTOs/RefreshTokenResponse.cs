namespace scorecard_user_mgt.DTOs
{
    public class RefreshTokenResponse
    {
        public string NewAccessToken { get; set; }
        public string NewRefreshToken { get; set; }
    }
}
