using API.Data;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }
        
        // TODO: Implement all Admin Methods
    }
}