using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedVn.Controllers
{

    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult ClientTest()
        {
            return View();
        }
    }
}