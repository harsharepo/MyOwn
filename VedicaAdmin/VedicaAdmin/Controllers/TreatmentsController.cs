using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VedicaModel;

namespace VedicaAdmin.Controllers
{
    public class TreatmentsController : Controller
    {
        //
        // GET: /Treatments/
        VedicaDBEntities context = new VedicaDBEntities();
        [Authorize]
        public ActionResult Index()
        {
            var lstTreatments = context.Treatments.ToList();
            foreach (var item in lstTreatments)
            {
                var treatmentTypeName = context.TreatmentTypes.Where(c => c.Id == item.TreatmentTypeId).FirstOrDefault();
                if (treatmentTypeName != null)
                {
                    lstTreatments.Where(c => c.Id == item.Id).FirstOrDefault().TreatmentTypes.Name = treatmentTypeName.Name;
                }
            }
            return View(lstTreatments.OrderBy(c => c.OrderId));
        }
        [Authorize]
        public JsonResult GetTreatmentTypes()
        {
            var lstTreatmentTypes = context.TreatmentTypes.ToList();
            return Json(lstTreatmentTypes, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public string SaveTreatments(int treatmentType, string treatmentName)
        {
            string isSaved = string.Empty;
            try
            {
                var maxOrderId = context.Treatments.Max(c => c.OrderId);
                maxOrderId = maxOrderId == null ? 1 : maxOrderId + 1;
                context.Treatments.Add(new Treatments
                {
                    CreatedDate = DateTime.Now,
                    Name = treatmentName,
                    TreatmentTypeId = treatmentType,
                    OrderId = maxOrderId
                });
                isSaved = "true";
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                isSaved = ex.Message;
            }
            return isSaved;
        }
        [Authorize]
        public string UpdateTreatments(int treatmentId, string treatmentName, int treatmentType)
        {
            string isUpdated = string.Empty;
            try
            {
                var treatments = context.Treatments.Where(c => c.Id == treatmentId).FirstOrDefault();
                if (treatments != null)
                {
                    treatments.Name = treatmentName;
                    treatments.TreatmentTypeId = treatmentType;
                    context.Entry(treatments).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    isUpdated = "true";
                }
            }
            catch (Exception ex)
            {
                isUpdated = ex.Message;
            }
            return isUpdated;
        }
        [Authorize]
        public string DeleteTreatments(int treatmentId)
        {
            string isDeleted = string.Empty;
            try
            {
                var treatments = context.Treatments.Where(c => c.Id == treatmentId).FirstOrDefault();
                if (treatments != null)
                {
                    var lstTreatmentDetails = context.TreatmentDetails.Where(c => c.TreatmentsId == treatments.Id);
                    foreach (var item in lstTreatmentDetails)
                    {
                        context.TreatmentDetails.Remove(item);
                    }
                    context.SaveChanges();

                    context.Treatments.Remove(treatments);
                    context.Entry(treatments).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                    isDeleted = "true";
                }
            }
            catch (Exception ex)
            {
                isDeleted = ex.Message;
            }
            return isDeleted;
        }
        public string UpdateOrder(string jsonArray)
        {
            string strReturn = string.Empty;
            JavaScriptSerializer jSerializer = new JavaScriptSerializer();
            List<Treatments> lstTreatments = jSerializer.Deserialize<List<Treatments>>(jsonArray);
            foreach (var item in lstTreatments)
            {
                context.Treatments.Where(c => c.Id == item.Id).FirstOrDefault().OrderId = item.OrderId;
            }
            context.SaveChanges();
            strReturn = "success";
            return strReturn;
        }
	}
}