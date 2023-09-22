namespace NSTask.Models.Dtos
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccest { get; set; }
    }
}
