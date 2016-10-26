using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VedicaModel;

namespace Vedica.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        VedicaDBEntities context = new VedicaDBEntities();
        public ActionResult Index()
        {
            var sliderImages = context.SliderImages.ToList();
            foreach (var item in sliderImages)
            {
                var imageFile = context.ImageFiles.Where(c => c.FileGuid == item.ImageId).FirstOrDefault();
                if (imageFile != null)
                {
                    var imagePath = imageFile.FilePath;
                    imagePath = imagePath.Substring(imagePath.IndexOf("SliderImages"), imagePath.Length - imagePath.IndexOf("SliderImages"));
                    imagePath = Properties.Settings.Default.SliderImagePath + imagePath.Replace("\\","//");
                    sliderImages.Where(c => c.ImageId == item.ImageId).FirstOrDefault().ImageFiles.FilePath = imagePath;
                }
            }
            return View(sliderImages);
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Ayurveda()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            var galleryImages = context.Gallery.ToList();
            foreach (var item in galleryImages)
            {
                ImageFiles imageFile = context.ImageFiles.Where(c => c.FileGuid == item.ImageId).FirstOrDefault();
                if (imageFile != null)
                {
                    var imagePath = imageFile.FilePath;
                    imagePath = imagePath.Substring(imagePath.IndexOf("GalleryImages"), imagePath.Length - imagePath.IndexOf("GalleryImages"));
                    imagePath = Properties.Settings.Default.SliderImagePath + imagePath.Replace("\\", "//");
                    galleryImages.Where(c => c.ImageId == item.ImageId).FirstOrDefault().ImageFiles.FilePath = imagePath;
                }
            }
            return View(galleryImages);
        }
        public ActionResult LoadOffers()
        {
            var offers = context.Offers.ToList();
            return PartialView("_Offers", offers);
        }
	}
    public class IndexData
    {
        public List<SliderImages> lstSliderImages { get; set; }
        public List<Offers> lstOffers { get; set; }
    }
}