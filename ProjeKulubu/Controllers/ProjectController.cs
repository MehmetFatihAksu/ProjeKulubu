using ProjeKulubu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeKulubu.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        public ActionResult CompleteProjects()
        {
            return View();
        }


    }
}
