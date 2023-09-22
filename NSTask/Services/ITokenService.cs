namespace NSTask.Services
{
    public class ITokenService
    {
        public Task<TokenResultDto> CreateToken(Guid id, string Email);
    }
}
