using Microsoft.AspNetCore.Mvc;
using SantaGift.DBContext;

namespace SantaGift.Controllers
{
    public class PlayGameController : Controller
    {
        private readonly RegisterDb _dbContext;
        public PlayGameController(RegisterDb db)
        {
            _dbContext = db;
        }
        public IActionResult PlayGame()
        {
            return View();
        }

        public IActionResult SpinWheel()
        {
            return View();
        }
    }
}
