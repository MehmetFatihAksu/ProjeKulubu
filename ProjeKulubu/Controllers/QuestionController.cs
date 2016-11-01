using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using ProjeKulubu.Models;

namespace ProjeKulubu.Controllers
{
    public class QuestionController : Controller
    {
        //
        // GET: /Question/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(AskedQuestions Model)
        {
            if (Model.Question != null && Model.QuestionAnswer != null)
            {
                db.AskedQuestions.Add(Model);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Lütfen alanları boş bırakmayınız";
            }

            return Json(new { Model = Model });
        }

        public ActionResult QuestionDetail(int id)
        {
            var data = db.AskedQuestions.Where(x => x.ID == id).FirstOrDefault();

            return View("QuestionList","Question",data);
        }


    }
}
