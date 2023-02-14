using Infrastructure.Dtos;
using Models.Entities;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser>GetUserById(int id);
    Task<AppUser>GetUserByUserName(string userName);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto> GetMemberAsync(string username);
}