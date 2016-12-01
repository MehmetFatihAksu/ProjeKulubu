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
        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        #region Views
        [UserAuthorize]
        public ActionResult TeamIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
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
        [UserAuthorize]
        public ActionResult TeamDelete(int id)
        {
            var data = db.Team.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult TeamUpdate(int id)
        {
            var data = db.Team.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        #endregion

        #region Methods
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTeamMember(int officeId,int projectId,string office, string name, string position, string exp, int age, HttpPostedFileBase picture,string seo, string facebook, string twitter, string google, string linkedin, string biografi)
        {
            Team teamModel = new Team();
            biografi = biografi.Replace("<p>", "").Replace("</p>", "");
            string fileMap = Path.GetFileName(picture.FileName);
            var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
            picture.SaveAs(loadLocation);
            teamModel.MemberAge = age;
            teamModel.MemberBiografi = biografi;
            teamModel.MemberExperience = exp;
            teamModel.MemberFacebookURL = "Http://" + facebook;
            teamModel.MemberGoogleURL = "Http://" + google;
            teamModel.MemberLinkedinURL = "Http://" + linkedin;
            teamModel.MemberName = name;
            teamModel.MemberPictureSEO = seo;
            teamModel.MemberPictureURL = fileMap;
            teamModel.MemberPozision = position;
            teamModel.MemberTwitterURL = "Http://" + twitter;

            if(officeId == 0)
            {

            }
            else
            {
                teamModel.OfficeID = officeId;
            }

            if(projectId == 0)
            {

            }
            else
            {
                teamModel.ProjectID = projectId;
            }

            db.Team.Add(teamModel);
            db.SaveChanges();
            return RedirectToAction("TeamIndex", "Team");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TeamDataUpdate(int id,int officeId,string name,string seo, string position, int age, string exp, HttpPostedFileBase picture, string facebook, string twitter, string google, string linkedin, string biografi)
        {
            Team updateModel = db.Team.Where(x => x.ID == id).FirstOrDefault();
            biografi = biografi.Replace("<p>", "").Replace("</p>", "");
            if (picture == null)
            {
                updateModel.MemberPictureSEO = seo;
                updateModel.MemberAge = age;
                updateModel.MemberBiografi = biografi;
                updateModel.MemberExperience = exp;
                updateModel.MemberFacebookURL = facebook;
                updateModel.MemberGoogleURL = google;
                updateModel.MemberLinkedinURL = linkedin;
                updateModel.MemberName = name;
                updateModel.MemberPozision = position;
                updateModel.MemberTwitterURL = twitter;
                updateModel.OfficeID = officeId;
                db.SaveChanges();
            }
            else
            {
                string fileMap = Path.GetFileName(picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                picture.SaveAs(loadLocation);
                updateModel.MemberPictureURL = fileMap;
                updateModel.MemberAge = age;
                updateModel.MemberBiografi = biografi;
                updateModel.MemberExperience = exp;
                updateModel.MemberFacebookURL = facebook;
                updateModel.MemberGoogleURL = google;
                updateModel.MemberLinkedinURL = linkedin;
                updateModel.MemberName = name;
                updateModel.MemberPictureSEO = seo;
                updateModel.MemberPozision = position;
                updateModel.MemberTwitterURL = twitter;
                updateModel.OfficeID = officeId;
                db.SaveChanges();
            }
            return RedirectToAction("TeamIndex", "Team");
        }

        [HttpPost]
        public ActionResult TeamDataDelete(int id)
        {
            Team teamMemberDelete = db.Team.Find(id);
            db.Team.Remove(teamMemberDelete);
            db.SaveChanges();
            return RedirectToAction("TeamIndex", "Team");
        }

        [HttpPost]
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Team.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Team.Remove(y));
            db.SaveChanges();
            return RedirectToAction("TeamIndex", "Team");
        }
        #endregion

    }
}
