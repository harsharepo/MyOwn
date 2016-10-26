using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VedicaModel;

namespace VedicaAdmin.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/
        VedicaDBEntities context = new VedicaDBEntities();
        public ActionResult Index()
        {
            var galleryImages = context.Gallery.ToList();
            foreach (var item in galleryImages)
            {
                var galleryImage = galleryImages.Where(c => c.Id == item.Id).FirstOrDefault();
                if (galleryImage != null)
                {
                    var imageFile = context.ImageFiles.Where(c => c.FileGuid == galleryImage.ImageId).FirstOrDefault();
                    if (imageFile != null)
                    {
                        var filePath = imageFile.FilePath.Substring(imageFile.FilePath.IndexOf("GalleryImages"), imageFile.FilePath.Length - imageFile.FilePath.IndexOf("GalleryImages"));
                        filePath = filePath.Replace("\\", "/");
                        filePath = Path.Combine("/", filePath);
                        galleryImages.Where(c => c.Id == item.Id).FirstOrDefault().ImageFiles.FilePath = filePath;
                    }
                }
            }
            return View(galleryImages);
        }
        public ActionResult Create()
        {
            return View();
        }
        public string UplaodFile(string fileName, string fileType, string size)
        {
            string fileReturnGuid = string.Empty;
            using (TransactionScope scope = new TransactionScope())
            {
                Gallery gal = new Gallery();
                string fileGuid = Guid.NewGuid().ToString();
                ImageFiles imgFiles = new ImageFiles();
                imgFiles.ContentType = Path.GetExtension(fileName);
                imgFiles.FileGuid = fileGuid;
                imgFiles.FileName = fileName;
                imgFiles.FilePath = Path.Combine(Server.MapPath("/GalleryImages"), fileGuid  + Path.GetExtension(fileName));
                imgFiles.Size = Convert.ToDecimal(size);

                context.ImageFiles.Add(imgFiles);
                context.SaveChanges();

                gal.ImageId = fileGuid;
                gal.Name = fileName;
                context.Gallery.Add(gal);
                context.SaveChanges();

                scope.Complete();

                fileReturnGuid = fileGuid + imgFiles.ContentType;
            }
            return fileReturnGuid;
        }
        public ActionResult DeleteImage(int id)
        {
            string data = "false";
            var galleryImage = context.Gallery.Where(c => c.Id == id).FirstOrDefault();
            if (galleryImage != null)
            {
                ImageFiles imgFile = context.ImageFiles.Where(c => c.FileGuid == galleryImage.ImageId).FirstOrDefault();
                if (imgFile != null)
                {
                    if (System.IO.File.Exists(imgFile.FilePath))
                    {
                        System.IO.File.Delete(imgFile.FilePath);
                    }
                    context.Gallery.Remove(galleryImage);
                    context.Entry(galleryImage).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();

                    context.ImageFiles.Remove(imgFile);
                    context.Entry(imgFile).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();

                    data = "true";
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public void ProcessRequest(string fileName)
        {
            Stream inputStream = Request.InputStream;
            byte[] bytesToRead = new byte[inputStream.Length];
            string FileName = Request.QueryString["fileName"];
            inputStream.Read(bytesToRead, 0, bytesToRead.Length);
            string filePath = Server.MapPath("~/GalleryImages/" + FileName);
            System.IO.FileStream fs = null;
            using (fs = !System.IO.File.Exists(filePath) ? System.IO.File.Create(filePath) : System.IO.File.Open(filePath, FileMode.Append))
            {
                fs.Write(bytesToRead, 0, bytesToRead.Length);
            }

        }
    }
}