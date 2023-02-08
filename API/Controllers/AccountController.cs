using Infrastructure.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly  DataDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public AccountController(DataDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExist(registerDTO.UserName)) return BadRequest("UserName is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDTO.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok(new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if (user is null)
                return Unauthorized();
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password");
            }
            return Ok(new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            });
        }
        private async Task<bool> UserExist(string userName)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}
