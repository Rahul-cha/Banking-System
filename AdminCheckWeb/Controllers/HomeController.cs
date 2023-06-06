using AdminCheckWeb.Data;
using AdminCheckWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AdminCheckWeb.Controllers
{
    public class HomeController : Controller
    {
        int UID;

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbcontext;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext dbcontext)
        {
            _logger = logger;
            _dbcontext    = dbcontext;
        }


        //GET
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                var allusers = _dbcontext.logins.ToList();
                var validuser = allusers.Where(y => y.username == loginView.UserName && y.password == loginView.Password).SingleOrDefault();
                if(validuser == null)
                {
                   
                    Console.WriteLine("Invalid Username or password");

                }
                else
                {
                    UID = validuser.Id;
                    if (validuser.UserType == "Admin")
                    {
                        Console.WriteLine("Welcome Admin");
                        
                        
                        return RedirectToAction("Admin", "Category");
                    }
                    else
                    {
                        Console.WriteLine("Welcome Customer");


                        return RedirectToAction("User", "Category", new {uid=UID});
                    }
                 
                }

            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Category()
        {
               return View();
        }

        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}