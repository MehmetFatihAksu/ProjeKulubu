using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class TeamController : Controller
    {
        //
        // GET: /Team/
        db2299D218BEEntities9 db = new db2299D218BEEntities9();

        [UserAuthorize]
        public ActionResult TeamIndex(int ? page)
        {
            ViewBag.MemberName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";

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

            var kayitlar = from x in db.Team select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.MemberName.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Team => Team.MemberName);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Team => Team.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTeamMember(string office, string name,string position,string exp,int age,HttpPostedFileBase picture,string facebook,string twitter,string google,string linkedin,string biografi)
        {
            Team teamModel = new Team();
            
            if(name!=null && position!=null && exp!=null && picture!=null && biografi!=null)
            {
                string fileMap = Path.GetFileName(picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                picture.SaveAs(loadLocation);
                teamModel.MemberAge = age;
                teamModel.MemberBiografi = biografi;
                teamModel.MemberExperience = exp;
                teamModel.MemberFacebookURL ="Http://"+facebook;
                teamModel.MemberGoogleURL = "Http://"+google;
                teamModel.MemberLinkedinURL = "Http://" +linkedin;
                teamModel.MemberName = name;
                teamModel.MemberPictureURL = fileMap;
                teamModel.MemberPozision = position;
                teamModel.MemberTwitterURL = "Http://" +twitter;
                db.Team.Add(teamModel);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("Belirlenemeyen bir hata oluştu");
            }
               
            
            return RedirectToAction("TeamIndex", "Team");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TeamDataUpdate(int id,string name,string position,int age,string exp,HttpPostedFileBase picture,string facebook,string twitter,string google,string linkedin,string biografi)
        {
            Team updateModel = db.Team.Where(x => x.ID == id).FirstOrDefault();
            if(name!=null && position!=null && exp!=null && biografi!=null)
            {
                string fileMap = Path.GetFileName(picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                picture.SaveAs(loadLocation);
                updateModel.MemberAge = age;
                updateModel.MemberBiografi = biografi;
                updateModel.MemberExperience = exp;
                updateModel.MemberFacebookURL = facebook;
                updateModel.MemberGoogleURL = google;
                updateModel.MemberLinkedinURL = linkedin;
                updateModel.MemberName = name;
                updateModel.MemberPictureURL = fileMap;
                updateModel.MemberPozision = position;
                updateModel.MemberTwitterURL = twitter;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("Belirlenemeyen bir hata oluştu");
            }
            return RedirectToAction("TeamIndex", "Team");
        }

        [HttpPost]
        public ActionResult TeamDataDelete(int id)
        {
            Team teamMemberDelete = db.Team.Find(id);
            db.Team.Remove(teamMemberDelete);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [UserAuthorize]
        public ActionResult TeamDelete(int id)
        {
            var data = db.Team.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult TeamView(int id)
        {
            var data = db.Team.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult TeamUpdate(int id)
        {
            db.Team.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Team.Remove(y));
            db.SaveChanges();
            return RedirectToAction("TeamIndex", "Team");
        }

    }
}
