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
        public ActionResult TeamIndex(int ? page)
        {
            var list = db.Team.ToList();
            var pageNumber = page ?? 1;
            var onePageOfTeam = list.ToPagedList(pageNumber, 10);
            ViewBag.Show = onePageOfTeam;
            return View();
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
                teamModel.MemberFacebookURL = facebook;
                teamModel.MemberGoogleURL = google;
                teamModel.MemberLinkedinURL = linkedin;
                teamModel.MemberName = name;
                teamModel.MemberPictureURL = fileMap;
                teamModel.MemberPozision = position;
                teamModel.MemberTwitterURL = twitter;
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
            if(name!=null && position!=null && exp!=null && picture!=null && biografi!=null)
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

        public ActionResult TeamDelete(int id)
        {
            var data = db.Team.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult TeamView(int id)
        {
            var data = db.Team.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult TeamUpdate(int id)
        {
            var data = db.Team.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }
}
