using Microsoft.EntityFrameworkCore;
using NSTask.Data;
using NSTask.Models.Dtos;
using NSTask.Models.Entities;

namespace NSTask.Services
{
    public class UserService:IUserService
    {
        private readonly NSDataBase _DbContext;
        private readonly ITokenService _tokenService;

        public UserService(NSDataBase Dbcontext, ITokenService tokenService)
        {
            _DbContext = Dbcontext;
            _tokenService = tokenService;
        }
        public async Task<LoginResultDto> LogIn(string email, string password)
        {
            var User = await _DbContext.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);
            if (User != null)
            {
                var token = await _tokenService.CreateToken(User.Id, User.Email);

                var result = new LoginResultDto
                {
                    Token = token.Token,
                    IsSuccest = true,
                };

                var saveToken = new UserToken
                {
                    TokenExp = token.TokenExp,
                    TokenHash = token.Token,
                    User = User,
                };

                _DbContext.Add<UserToken>(saveToken);
                _DbContext.SaveChanges();

                return result;
            }
            else
            {
                var result = new LoginResultDto
                {
                    IsSuccest = false,
                };

                return result;
            }
        }

        public async Task<bool> SingUp(SingUpDto dto)
        {
            var newUser = new Users
            {
                Email = dto.Email,
                Password = dto.Password,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
            };

            await _DbContext.AddAsync<Users>(newUser);
            await _DbContext.SaveChangesAsync();

            return true;
        }
    }
}
