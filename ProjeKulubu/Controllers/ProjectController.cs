using ProjeKulubu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity.Infrastructure;
using PagedList;


namespace ProjeKulubu.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        db2299D218BEEntities9 db = new db2299D218BEEntities9();

        public ActionResult CompleteProjects(int? page)
        {
            var list = db.Project.ToList();
            var pageNumber = page ?? 1;
            var onePageOfProject = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfProject;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCompleteProject(string Name, string Type, string Content, string Bugdet, string Birim, string Year, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Project project = new Project();

            Location = "İstanbul,Türkiye";

            Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name != null || Type != null || Birim != null || Content != null || Bugdet != null)
            {
                project.ProjectName = Name;
                project.ProjectBudgets = Bugdet;
                project.ProjectContent = Content;
                project.ProjectMoneyType = Birim;
                project.ProjectYear = Year;
                project.ProjectMail = EMail;
                project.ProjectLocation = Adres + " " + Location;
                project.ProjectType = Type;
                project.ProjectStatusID = 1;
                project.ProjectPhone = Telefon;
                project.ProjectAltLocation = Adres;

                db.Project.Add(project);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin.";
            }
            return RedirectToAction("CompleteProjects", "Project");
        }

        public ActionResult CompleteProjectUpdate(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult CompleteProjectDelete(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CompleteProjectDataUpdate(int id ,string Name, string Type, string Content, string Bugdet, string Birim, string Year, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Project projectModel = db.Project.Where(x => x.ID == id).FirstOrDefault();

            Location = "İstanbul,Türkiye";

            Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name != null || Type != null || Birim != null || Content != null || Bugdet != null)
            {
                projectModel.ProjectName = Name;
                projectModel.ProjectBudgets = Bugdet;
                projectModel.ProjectContent = Content;
                projectModel.ProjectMoneyType = Birim;
                projectModel.ProjectYear = Year;
                projectModel.ProjectMail = EMail;
                projectModel.ProjectLocation = Adres + " " + Location;
                projectModel.ProjectType = Type;
                projectModel.ProjectStatusID = 1;
                projectModel.ProjectPhone = Telefon;
                projectModel.ProjectAltLocation = Adres;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin.";
            }
            return RedirectToAction("CompleteProjects", "Project");
        }


        [HttpPost]
        public ActionResult CompleteProjectDataDelete(int id)
        {
            Project proje = db.Project.Find(id);
            db.Project.Remove(proje);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult ProjectPictureDataDelete(int id)
        {
            ProjectPicture projectPicture = db.ProjectPicture.Find(id);
            db.ProjectPicture.Remove(projectPicture);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }


        public ActionResult ProjectPictureIndex(int ? page)
        {
            var list = db.ProjectPicture.ToList();
            var pageNumber = page ?? 1;
            var onePageOfProjectPicture = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfProjectPicture;
            return View();
        }


        public ActionResult ProjectPictureUpdate(int id)
        {
            var data = db.ProjectPicture.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult ProjectPictureDelete(int id)
        {
            var data = db.ProjectPicture.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }


        [HttpPost]
        public ActionResult ProjectPictureDataUpdate(int id, int projectId, HttpPostedFileBase ProjectPicture, string ProjectPictureSEO, string ProjectPictureDesc)
        {
            ProjectPicture projePictureModel = db.ProjectPicture.Where(x=>x.ID == id).FirstOrDefault();

            if (projectId !=null || ProjectPicture!=null)
	        {
                 string fileMap = Path.GetFileName(ProjectPicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                ProjectPicture.SaveAs(loadLocation);

                projePictureModel.PictureSEO = ProjectPictureSEO;
                projePictureModel.PictureALT = ProjectPictureDesc;
                projePictureModel.ProjectID = projectId;
                projePictureModel.PictureURL = fileMap;
                db.SaveChanges();
		 
	        }else{
                   ViewBag.Error = "Lütfen eksiksiz veri girişi yapınız..";
            }
            return RedirectToAction("ProjectPictureIndex", "Project");
        }

        [HttpPost]
        public ActionResult AddProjectPicture(int projectId, HttpPostedFileBase ProjectPicture, string ProjectPictureSEO, string ProjectPictureDesc)
        {
            ProjectPicture projectPicture = new ProjectPicture();

            if (ProjectPicture != null || projectId != null)
            {
                string fileMap = Path.GetFileName(ProjectPicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                ProjectPicture.SaveAs(loadLocation);

                projectPicture.ProjectID = projectId;
                projectPicture.PictureSEO = ProjectPictureSEO;
                projectPicture.PictureALT = ProjectPictureDesc;
                projectPicture.PictureURL = fileMap;

                db.ProjectPicture.Add(projectPicture);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Lütfen eksiksiz veri girişi yapınız..";
            }
            return RedirectToAction("ProjectPictureIndex", "Project");
        }
        



    }
}
