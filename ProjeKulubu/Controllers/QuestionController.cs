﻿using System;
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

        db2299D218BEEntities8 db = new db2299D218BEEntities8();


        public ActionResult QuestionIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.Question = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";

            ViewBag.CurrentSort = Sorting_Order;
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            var kayitlar = from x in db.AskedQuestions select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.Question.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(AskedQuestions => AskedQuestions.Question);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(AskedQuestions => AskedQuestions.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
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
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.AskedQuestions.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.AskedQuestions.Remove(y));
            db.SaveChanges();
            return RedirectToAction("QuestionIndex", "Question");
        }


    }
}
