namespace scorecard_user_mgt.DTOs
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; internal set; }
    }   
}