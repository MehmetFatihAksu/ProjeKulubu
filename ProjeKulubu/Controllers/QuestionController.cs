using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ProjeKulubu.Models;
using System.Data.Entity.Infrastructure;

namespace ProjeKulubu.Controllers
{
    [ValidateInput(false)]
    public class QuestionController : Controller
    {
        //
        // GET: /Question/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();


        public ActionResult QuestionIndex()
        {
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
        public ActionResult QuestionDataUpdate(AskedQuestions Model)
        {
            if (Model.ID != null)
            {
                var Query = from question in db.AskedQuestions
                            where question.ID == Model.ID
                            select question;


                foreach (AskedQuestions item in Query)
                {
                    item.Question = Model.Question;
                    item.QuestionAnswer = Model.QuestionAnswer;
                }
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Güncelle sırasında hata oluştu.Lütfen tekrar deneyiniz";
            }

            return Json(new { Model = Model });
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
