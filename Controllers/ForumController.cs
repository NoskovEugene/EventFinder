using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventFinder.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EventFinder.Controllers
{
    [Authorize()]
    [Route("forum/[action]")]
    public class ForumController : Controller
    {
        
        public IActionResult Forums()
        {
            return View();
        }
    }
}