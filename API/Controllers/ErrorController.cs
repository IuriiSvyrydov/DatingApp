namespace API.Controllers;

    public class ErrorController : BaseApiController
    {
        private readonly DataDbContext _context;

        public ErrorController(DataDbContext context)
        {
            _context = context;
        }

         [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret";

        }
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {

        var findUser = _context.Users.Find(-1);
            if (findUser == null) return NotFound();
            return findUser;
      


    }
    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {

        var findUser = _context.Users.Find(-1);

            var userToReturn = findUser.ToString();
            return userToReturn;

            
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This is badRequest");
    }
}

