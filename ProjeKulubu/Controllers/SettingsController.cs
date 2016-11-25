using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeKulubu.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/

        [UserAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
