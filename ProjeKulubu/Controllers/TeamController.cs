using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;

namespace ProjeKulubu.Controllers
{
    public class TeamController : Controller
    {
        //
        // GET: /Team/
        db2299D218BEEntities8 db = new db2299D218BEEntities8();
        public ActionResult TeamIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTeamMember(string name,string position,string exp,int age,HttpPostedFileBase picture,string facebook,string twitter,string google,string linkedin,string biografi)
        {
            Team teamModel = new Team();
          
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
            
            return RedirectToAction("TeamIndex", "Team");
        }

        [HttpPost]
        public ActionResult TeamDataUpdate(int Id ,string name, string position, string exp, int age, HttpPostedFileBase picture, string facebook, string twitter, string google, string linkedin, string biografi)
        {
            if (Id !=null)
            {
                var Query = from memberData in db.Team
                            where
                                memberData.ID == Id
                            select memberData;


                foreach (Team item in Query)
                {
                    item.MemberName = name;
                    item.MemberPozision = position;
                    item.MemberExperience = exp;
                    item.MemberAge = age;
                    item.MemberPictureURL = picture.FileName;
                    item.MemberFacebookURL = facebook;
                    item.MemberTwitterURL = twitter;
                    item.MemberGoogleURL = google;
                    item.MemberLinkedinURL = linkedin;
                    item.MemberBiografi = biografi;
                }
                db.SaveChanges();
            }
            else
            {
                return View("ErrorPage", "Error");
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
