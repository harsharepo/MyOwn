using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using VedicaModel;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HelperClasses;

namespace VedicaAdmin.Controllers
{
    public class SliderImageController : Controller
    {
        VedicaDBEntities context = new VedicaDBEntities();

        public ActionResult Index()
        {
            var sliderImages = context.SliderImages.ToList();
            var lstSliderImages = new List<SliderImages>();
            foreach (var item in sliderImages)
            {
                var imageFile = context.ImageFiles.Where(c => c.FileGuid == item.ImageId).FirstOrDefault();
                if (imageFile != null)
                {
                    item.ImageFiles.FilePath = imageFile.FilePath;
                    lstSliderImages.Add(item);
                }
            }
            return View(lstSliderImages);
        }


        public ActionResult AddImages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImages(SliderImages sldrImages, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                CloudiNarySetup cldNSetup = new CloudiNarySetup();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var file in files)
                    {
                        string filePath = cldNSetup.SaveToCloud(file);

                        string fileGuid = Guid.NewGuid().ToString();
                        ImageFiles imgFiles = new ImageFiles();
                        imgFiles.ContentType = Path.GetExtension(file.FileName);
                        imgFiles.FileGuid = fileGuid;
                        imgFiles.FileName = file.FileName;
                        imgFiles.FilePath = filePath;
                        imgFiles.Size = file.ContentLength;

                        context.ImageFiles.Add(imgFiles);
                        context.SaveChanges();

                        sldrImages.ImageId = fileGuid;
                        sldrImages.CreatedDate = DateTime.Now;
                        context.SliderImages.Add(sldrImages);
                        context.SaveChanges();
                    }
                    scope.Complete();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteImage(int id)
        {
            string strResult = string.Empty;
            var sliderImage = context.SliderImages.Where(c => c.SliderImageId == id).FirstOrDefault();
            using (TransactionScope scope = new TransactionScope())
            {
                if (sliderImage != null)
                {
                    ImageFiles imgFile = context.ImageFiles.Where(c => c.FileGuid == sliderImage.ImageId).FirstOrDefault();
                    if (imgFile != null)
                    {
                        if (System.IO.File.Exists(imgFile.FilePath))
                        {
                            System.IO.File.Delete(imgFile.FilePath);
                        }
                        context.SliderImages.Remove(sliderImage);
                        context.Entry(sliderImage).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();

                        context.ImageFiles.Remove(imgFile);
                        context.Entry(imgFile).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                    }
                }
                scope.Complete();
                strResult = "true";
            }

            return Json(strResult, JsonRequestBehavior.AllowGet);
        }
    }
}