using AutoMapper;
using Infrastructure.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly  DataDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(DataDbContext dbContext, ITokenService tokenService, IMapper mapper)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExist(registerDTO.UserName)) return BadRequest("UserName is taken");

            var user = _mapper.Map<AppUser>(registerDTO);
            using var hmac = new HMACSHA512();


            user.UserName = registerDTO.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
            user.PasswordSalt = hmac.Key;
            
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok(new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs
                
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
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x=>x.IsMain)?.Url,
                KnownAs = user.KnownAs
            });
        }
        private async Task<bool> UserExist(string userName)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}
