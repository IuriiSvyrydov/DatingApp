
using System.Security.Claims;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, IPhotoService photoService)
        {
            _userService = userService;
            _mapper = mapper;
            _photoService = photoService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task< ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userService.GetMembersAsync();
            //var userToMap = _mapper.Map<IEnumerable<MemberDto>>(users); 
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            var userToMap = _mapper.Map<MemberDto>(user);
            return Ok(userToMap);
        }


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            
            return Ok(await _userService.GetMemberAsync(username));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _userService.GetUserByUserName(User.GetUserName());

            if (user is null) return NotFound();
            _mapper.Map(memberUpdateDto, user);
            if (await _userService.SaveAllAsync()) return NoContent();
            
          
            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userService.GetUserByUserName(User.GetUserName());

            if (user == null) return NotFound();
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error is not null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                RepublicId = result.PublicId
            };
            if (user.Photos.Count==0) photo.IsMain = true;
            user.Photos.Add(photo);
            if (await _userService.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetUser),new {username=user.UserName},
                                    _mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Problem adding Photo");

        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userService.GetUserByUserName(User.GetUserName());
            if (user is null) return NotFound();
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null) return NotFound();
            if (photo.IsMain) return BadRequest("This is already your main photo");
            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;
            if (await _userService.SaveAllAsync()) return NoContent();
            return BadRequest("Problem settings thr main photo");




        }
    }

    
}
