using ProjeKulubu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ProjeKulubu.Controllers
{
    public class OfficeController : Controller
    {

        db2299D218BEEntities8 db = new db2299D218BEEntities8();
        
        public ActionResult OfficeIndex()
        {
            return View();
        }

       
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult AddOffice(string Name, string Content, List<string> PersonData, string AltIcerik, string Zaman, string Telefon, string EMail, string Konum, string Adres, HttpPostedFileBase uploadFile, HttpPostedFileBase uploadFile1, HttpPostedFileBase uploadFile2, HttpPostedFileBase uploadFile3, HttpPostedFileBase uploadFile4)
        //{
        //    Office office = new Office();

        //    if (Content!=null && AltIcerik !=null )
        //    {
        //        Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r","").Replace("\n","");
        //        AltIcerik = AltIcerik.Replace("<p>", "").Replace("</p>", "").Replace("\r","").Replace("\n","");
                
        //        office.OfficeMainContent = Content;
        //        office.OfficeAltContent = AltIcerik;
               
        //    }
        //    if (PersonData.Count > 0)
        //    {
                
        //    }

        //    if (uploadFile !=null)
        //    {
        //        string fileMap = Path.GetFileName(uploadFile.FileName);
        //        var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"),fileMap);
        //        uploadFile.SaveAs(loadLocation);
        //    }



        //    for (int i = 1; i < 5; i++)
        //    {
        //        var uploadFileData = uploadFile + i.ToString();

        //    }
          


        //    return View();
        //}

    }
}
