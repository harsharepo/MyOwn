using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VedicaModel;
using MailSending;

namespace Vedica.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        VedicaDBEntities context = new VedicaDBEntities();
        MergedIndexClasses mrgdClasses = new MergedIndexClasses();
        public ActionResult Index()
        {
            var sliderImages = context.SliderImages.ToList();
            var trDetails = context.TreatmentDetails.ToList();
            foreach (var item in sliderImages)
            {
                var imageFile = context.ImageFiles.Where(c => c.FileGuid == item.ImageId).FirstOrDefault();
                if (imageFile != null)
                {
                    //var imagePath = imageFile.FilePath;
                    //imagePath = imagePath.Substring(imagePath.IndexOf("SliderImages"), imagePath.Length - imagePath.IndexOf("SliderImages"));
                    //imagePath = Properties.Settings.Default.SliderImagePath + imagePath.Replace("\\","//");
                    sliderImages.Where(c => c.ImageId == item.ImageId).FirstOrDefault().ImageFiles.FilePath = imageFile.FilePath;
                }
            }
            foreach (var item in trDetails)
            {
                var imageFile = context.ImageFiles.Where(c => c.FileGuid == item.ImageId).FirstOrDefault();
                if (imageFile != null)
                {
                    //var imagePath = imageFile.FilePath;
                    //imagePath = imagePath.Substring(imagePath.IndexOf("SliderImages"), imagePath.Length - imagePath.IndexOf("SliderImages"));
                    //imagePath = Properties.Settings.Default.SliderImagePath + imagePath.Replace("\\","//");
                    trDetails.Where(c => c.ImageId == item.ImageId).FirstOrDefault().ImageFiles.FilePath = imageFile.FilePath;
                }
            }
            mrgdClasses.sldrImages = sliderImages;
            mrgdClasses.treatDetails = trDetails.Where(c => c.Visibility == "Y").ToList();
            return View(mrgdClasses);
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

                    galleryImages.Where(c => c.ImageId == item.ImageId).FirstOrDefault().ImageFiles.FilePath = imageFile.FilePath;
                }
            }
            return View(galleryImages);
        }
        public ActionResult LoadOffers()
        {
            var offers = context.Offers.ToList();
            return PartialView("_Offers", offers);
        }
        public ActionResult SendEmail(string name, string phone, string email, string app_date, string center, string message)
        {
            string messageHtml = "Name : " + name + "<br />" + "Phone : " + phone + "<br />" + "Email : " + email + "<br />" + "Appointment Date : " + app_date + "<br />" + "Center : " + center + "<br />" + "Message : " + message;
            SendMail sendMail = new SendMail();
            string result = sendMail.sendMail(name, email, messageHtml);
            ViewBag.MailError = result;
            return RedirectToAction("Index");
        }
	}
    public class MergedIndexClasses
    {
        public List<SliderImages> sldrImages = new List<SliderImages>();
        public List<TreatmentDetails> treatDetails = new List<TreatmentDetails>();
    }
}