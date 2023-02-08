using Models.Entities;

namespace Infrastructure.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
