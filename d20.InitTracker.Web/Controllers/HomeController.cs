using d20.InitTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using d20.InitTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace d20.InitTracker.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ILogger<HomeController> _logger;
        private readonly D20ProjectsContext _context;


        public HomeController(ILogger<HomeController> logger, GraphServiceClient graphServiceClient, D20ProjectsContext context)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;;
            _context = context;
        }

        [AuthorizeForScopes(ScopeKeySection = "MicrosoftGraph:Scopes")]
        public async Task<IActionResult> Index()
        {
            var user = await _graphServiceClient.Me.Request().GetAsync();
            ViewData["GraphApiResult"] = user.DisplayName;

            IndexViewModel vm = new IndexViewModel();

            vm.encounters = await _context.Encounters.ToListAsync();
            vm.combatants = await _context.Combatants.ToListAsync();

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
