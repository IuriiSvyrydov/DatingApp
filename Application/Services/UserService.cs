

namespace Application.Services;

public class UserService: IUserService
{
    private readonly  DataDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(DataDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Update(AppUser user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _dbContext.Users
            .Include(p=>p.Photos)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserById(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUserName(string userName)
    {
        return await _dbContext.Users.
            Include(p=>p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == userName);
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _dbContext.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await _dbContext.Users.Where(x => x.UserName == username
        ).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).
            SingleOrDefaultAsync();
    }
}