using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedVn.Controllers
{
    public class TestThemeController : Controller
    {
        // GET: TestTheme
        public ActionResult IndexBackend()
        {
            return View("~/Views/Backend/Dashboard.cshtml");
        }

        public ActionResult IndexFrontend()
        {
            return View("~/Views/Frontend/Home.cshtml");
        }

        public ActionResult DemoForm()
        {
            return View("~/Views/Backend/DemoForm2.cshtml");
        }

        public ActionResult DemoDatatable()
        {
            return View("~/Views/Backend/DemoDatatable.cshtml");
        }

        public ActionResult DetailPost()
        {
            return View("~/Views/Frontend/Post.cshtml");
        }

        public ActionResult ContactClient()
        {
            return View("~/Views/Frontend/Contact.cshtml");
        }

        public ActionResult CategoryClient()
        {
            return View("~/Views/Frontend/Category.cshtml");
        }
    }
}