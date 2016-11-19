using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ProjeKulubu.Models;
using System.Data.Entity.Infrastructure;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class QuestionController : Controller
    {
        //
        // GET: /Question/

        db2299D218BEEntities9 db = new db2299D218BEEntities9();


        public ActionResult QuestionIndex(int ? page)
        {
            var list = db.AskedQuestions.ToList();
            var pageNumber = page ?? 1;
            var onePageOfQuestion = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfQuestion;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddQuestion(string Question,string Answer)
        {
            AskedQuestions askModel = new AskedQuestions();
            if(Question!=null && Answer!=null)
            {
                askModel.Question = Question;
                askModel.QuestionAnswer = Answer;
                db.AskedQuestions.Add(askModel);
                db.SaveChanges();
            }
            return RedirectToAction("QuestionIndex", "Question");
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QuestionDataUpdate(int id,string Question,string Answer)
        {
            AskedQuestions updateModel = db.AskedQuestions.Where(x => x.ID == id).FirstOrDefault();
            if(Question!=null && Answer!=null)
            {
                updateModel.Question = Question;
                updateModel.QuestionAnswer = Answer;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("işlem başarısız");
            }
            return RedirectToAction("QuestionIndex", "Question");
        }


        [HttpPost]
        public ActionResult QuestionDataDelete(int id)
        {
            AskedQuestions Question = db.AskedQuestions.Find(id);
            db.AskedQuestions.Remove(Question);
            db.SaveChanges();

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }


        public ActionResult QuestionDelete(int id)
        {
            var data = db.AskedQuestions.Where(x => x.ID == id).FirstOrDefault();

            return View(data);
        }


        public ActionResult QuestionUpdate(int id)
        {
            var data = db.AskedQuestions.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult QuestionView(int id)
        {
            var data = db.AskedQuestions.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }


    }
}
