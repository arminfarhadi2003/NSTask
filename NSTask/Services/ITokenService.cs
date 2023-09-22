using NSTask.Models.Dtos;

namespace NSTask.Services
{
    public interface ITokenService
    {
        public Task<TokenResultDto> CreateToken(Guid id, string Email);
    }
}
