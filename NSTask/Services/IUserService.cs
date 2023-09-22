namespace NSTask.Services
{
    public interface IUserService
    {
        public Task<LoginResultDto> LogIn(string email, string password);
        public Task<bool> LogOut(string token);
        public Task<bool> SingUp(SingUpDto dto);
    }
}
