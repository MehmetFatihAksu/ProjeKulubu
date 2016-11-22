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


        #region Tamamlanan Proje Yönetimi
        [UserAuthorize]
        public ActionResult CompleteProjects(int? page)
        {
            var list = db.Project.Where(x => x.ProjectStatusID == 1).ToList();
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
        [UserAuthorize]
        public ActionResult CompleteProjectUpdate(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult CompleteProjectDelete(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CompleteProjectDataUpdate(int id, string Name, string Type, string Content, string Bugdet, string Birim, string Year, string Zaman, string Telefon, string EMail, string Location, string Adres)
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

        #endregion

        #region Proje Görsel Yönetimi
        [HttpPost]
        public ActionResult ProjectPictureDataDelete(int id)
        {
            ProjectPicture projectPicture = db.ProjectPicture.Find(id);
            db.ProjectPicture.Remove(projectPicture);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }


        public ActionResult ProjectPictureIndex(int? page)
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
            ProjectPicture projePictureModel = db.ProjectPicture.Where(x => x.ID == id).FirstOrDefault();

            if (projectId != null || ProjectPicture != null)
            {
                string fileMap = Path.GetFileName(ProjectPicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                ProjectPicture.SaveAs(loadLocation);

                projePictureModel.PictureSEO = ProjectPictureSEO;
                projePictureModel.PictureALT = ProjectPictureDesc;
                projePictureModel.ProjectID = projectId;
                projePictureModel.PictureURL = fileMap;
                db.SaveChanges();

            }
            else
            {
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



        #endregion


        #region Satışı Devam Eden Proje Yönetimi

        [UserAuthorize]
        public ActionResult SaleProjects(int? page)
        {
            var list = db.Project.Where(x => x.ProjectStatusID == 2).ToList();
            var pageNumber = page ?? 1;
            var onePageOfProject = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfProject;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSaleProject(string Name, string Type, string Content, string Bugdet, string Birim, string Year, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Project saleProject = new Project();

            Location = "İstanbul,Türkiye";

            Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name != null || Type != null || Birim != null || Content != null || Bugdet != null)
            {
                saleProject.ProjectName = Name;
                saleProject.ProjectBudgets = Bugdet;
                saleProject.ProjectContent = Content;
                saleProject.ProjectMoneyType = Birim;
                saleProject.ProjectYear = Year;
                saleProject.ProjectMail = EMail;
                saleProject.ProjectLocation = Adres + " " + Location;
                saleProject.ProjectType = Type;
                saleProject.ProjectStatusID = 2;
                saleProject.ProjectPhone = Telefon;
                saleProject.ProjectAltLocation = Adres;

                db.Project.Add(saleProject);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin.";
            }
            return RedirectToAction("SaleProjects", "Project");
        }
        [UserAuthorize]
        public ActionResult SaleProjectUpdate(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaleProjectDataUpdate(int id, string Name, string Type, string Content, string Bugdet, string Birim, string Year, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Project saleProjectModel = db.Project.Where(x => x.ID == id).FirstOrDefault();

            Location = "İstanbul,Türkiye";

            Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name != null || Type != null || Birim != null || Content != null || Bugdet != null)
            {
                saleProjectModel.ProjectName = Name;
                saleProjectModel.ProjectBudgets = Bugdet;
                saleProjectModel.ProjectContent = Content;
                saleProjectModel.ProjectMoneyType = Birim;
                saleProjectModel.ProjectYear = Year;
                saleProjectModel.ProjectMail = EMail;
                saleProjectModel.ProjectLocation = Adres + " " + Location;
                saleProjectModel.ProjectType = Type;
                saleProjectModel.ProjectStatusID = 2;
                saleProjectModel.ProjectPhone = Telefon;
                saleProjectModel.ProjectAltLocation = Adres;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin.";
            }
            return RedirectToAction("SaleProjects", "Project");
        }

        [UserAuthorize]
        public ActionResult SaleProjectDelete(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult SaleProjectDataDelete(int id)
        {
            Project SaleProject = db.Project.Find(id);
            db.Project.Remove(SaleProject);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        


        #endregion

        #region Yakındaki Projeler Yönetimi

        [UserAuthorize]
        public ActionResult SoonProjects(int? page)
        {
            var list = db.Project.Where(x => x.ProjectStatusID == 3).ToList();
            var pageNumber = page ?? 1;
            var onePageOfProject = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfProject;
            return View();
        }

        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSoonProject(string Name, string Type, string Content, string Bugdet, string Birim, string Year, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Project soonProject = new Project();

            Location = "İstanbul,Türkiye";

            Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name != null || Type != null || Birim != null || Content != null || Bugdet != null)
            {
                soonProject.ProjectName = Name;
                soonProject.ProjectBudgets = Bugdet;
                soonProject.ProjectContent = Content;
                soonProject.ProjectMoneyType = Birim;
                soonProject.ProjectYear = Year;
                soonProject.ProjectMail = EMail;
                soonProject.ProjectLocation = Adres + " " + Location;
                soonProject.ProjectType = Type;
                soonProject.ProjectStatusID = 3;
                soonProject.ProjectPhone = Telefon;
                soonProject.ProjectAltLocation = Adres;

                db.Project.Add(soonProject);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin.";
            }
            return RedirectToAction("SoonProjects", "Project");
        }
        [UserAuthorize]
        public ActionResult SoonProjectUpdate(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult SoonProjectDelete(int id)
        {
            var data = db.Project.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SoonProjectDataUpdate(int id, string Name, string Type, string Content, string Bugdet, string Birim, string Year, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Project soonProjectModel = db.Project.Where(x => x.ID == id).FirstOrDefault();

            Location = "İstanbul,Türkiye";

            Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name != null || Type != null || Birim != null || Content != null || Bugdet != null)
            {
                soonProjectModel.ProjectName = Name;
                soonProjectModel.ProjectBudgets = Bugdet;
                soonProjectModel.ProjectContent = Content;
                soonProjectModel.ProjectMoneyType = Birim;
                soonProjectModel.ProjectYear = Year;
                soonProjectModel.ProjectMail = EMail;
                soonProjectModel.ProjectLocation = Adres + " " + Location;
                soonProjectModel.ProjectType = Type;
                soonProjectModel.ProjectStatusID = 3;
                soonProjectModel.ProjectPhone = Telefon;
                soonProjectModel.ProjectAltLocation = Adres;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin.";
            }
            return RedirectToAction("SoonProjects", "Project");
        }
        
        [HttpPost]
        public ActionResult SoonProjectDataDelete(int id)
        {
            Project soonProject = db.Project.Find(id);
            db.Project.Remove(soonProject);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        #endregion



    }
}
