using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using VedicaModel;
using System.IO;
using HelperClasses;
using System.Transactions;
using System.Web.Script.Serialization;

namespace VedicaAdmin.Controllers
{
    public class TreatmentController : Controller
    {
        VedicaDBEntities context = new VedicaDBEntities();
        CloudiNarySetup cldSetup = new CloudiNarySetup();
        //
        // GET: /Treatment/
        [Authorize]
        public ActionResult Index()
        {
            var treatmentDetails = context.TreatmentDetails.ToList();
            foreach (var item in treatmentDetails)
            {
                var treatmentsName = context.Treatments.Where(c => c.Id == item.TreatmentsId).FirstOrDefault();
                var imageFile = context.ImageFiles.Where(c => c.FileGuid == item.ImageId).FirstOrDefault();
                if (treatmentsName != null)
                {
                    treatmentDetails.Where(c => c.Id == item.Id).FirstOrDefault().Treatments.Name = treatmentsName.Name;
                    if (imageFile != null)
                    {
                        var filePath = imageFile.FilePath;

                        treatmentDetails.Where(c => c.Id == item.Id).FirstOrDefault().ImageFiles.FilePath = filePath;
                    }
                }
            }
            return View(treatmentDetails);
        }
        [Authorize]
        public ActionResult Create()
        {
            var allTreatments = context.Treatments.ToList();
            var slTreatments = new List<SelectListItem>();
            foreach (var item in allTreatments)
            {
                SelectListItem i = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                };
                slTreatments.Add(i);
            }
            ViewBag.Treatments = slTreatments;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(TreatmentDetails treatmentDetails, HttpPostedFileBase file)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string filePath = cldSetup.SaveToCloud(file);
                ImageFiles imgFile = new ImageFiles();
                string fileGuid = Guid.NewGuid().ToString();

                imgFile.ContentType = System.IO.Path.GetExtension(file.FileName);
                imgFile.FileName = System.IO.Path.GetFileName(file.FileName);
                imgFile.FilePath = filePath;
                imgFile.Size = file.InputStream.Length;
                imgFile.FileGuid = fileGuid;

                context.ImageFiles.Add(imgFile);
                context.SaveChanges();

                var maxOrderId = context.TreatmentDetails.Max(c => c.OrderId);
                maxOrderId = maxOrderId == null ? 1 : maxOrderId + 1;
                treatmentDetails.CreatedDate = DateTime.Now;
                treatmentDetails.ImageId = fileGuid;
                treatmentDetails.OrderId = maxOrderId;
                context.TreatmentDetails.Add(treatmentDetails);
                context.SaveChanges();
                scope.Complete();
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public ActionResult Edit(int Id)
        {
            var treatmentDetails = context.TreatmentDetails.Where(c => c.Id == Id).FirstOrDefault();
            treatmentDetails.Treatments = context.Treatments.Where(c => c.Id == treatmentDetails.TreatmentsId).FirstOrDefault();
            treatmentDetails.ImageFiles = context.ImageFiles.Where(c => c.FileGuid == treatmentDetails.ImageId).FirstOrDefault();
            var allTreatments = context.Treatments.ToList();
            var slTreatments = new List<SelectListItem>();
            foreach (var item in allTreatments)
            {
                bool isSelected = item.Id == treatmentDetails.TreatmentsId ? true : false;
                SelectListItem i = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = isSelected
                };
                slTreatments.Add(i);
            }
            ViewBag.Treatments = slTreatments;
            ViewBag.TreatmentName = treatmentDetails.Name;
            return View(treatmentDetails);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(TreatmentDetails treatmentDetails, HttpPostedFileBase file)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (file != null)
                {
                    string filePath = cldSetup.SaveToCloud(file);
                    ImageFiles imgFile = new ImageFiles();
                    string fileGuid = Guid.NewGuid().ToString();

                    imgFile.ContentType = System.IO.Path.GetExtension(file.FileName);
                    imgFile.FileName = System.IO.Path.GetFileName(file.FileName);
                    imgFile.FilePath = filePath;
                    imgFile.Size = file.InputStream.Length;
                    imgFile.FileGuid = fileGuid;

                    context.ImageFiles.Add(imgFile);
                    context.SaveChanges();

                    treatmentDetails.ImageId = fileGuid;
                }

                //var maxOrderId = context.TreatmentDetails.Max(c => c.OrderId);
                //maxOrderId = maxOrderId == null ? 1 : maxOrderId + 1;
                treatmentDetails.CreatedDate = DateTime.Now;
                //treatmentDetails.OrderId = maxOrderId;
                context.Entry(treatmentDetails).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                scope.Complete();
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public string DeleteTreatmentDetails(int id)
        {
            string isDeleted = string.Empty;
            var treatmentDetails = context.TreatmentDetails.Where(c => c.Id == id).FirstOrDefault();
            if (treatmentDetails != null)
            {
                context.TreatmentDetails.Remove(treatmentDetails);
                context.Entry(treatmentDetails).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                isDeleted = "true";
            }
            return isDeleted;
        }
        public string SaveVisibility(string json)
        {
            string returnResult = string.Empty;
            try
            {
                JavaScriptSerializer jSerialze = new JavaScriptSerializer();
                List<TreatmentDetails> treatDetails = jSerialze.Deserialize<List<TreatmentDetails>>(json);
                foreach (var item in treatDetails)
                {
                    context.TreatmentDetails.Where(c => c.Id == item.Id).FirstOrDefault().Visibility = item.Visibility;
                }
                context.SaveChanges();
                returnResult = "success";
            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }
            return returnResult;
        }
	}
}