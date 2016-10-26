using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VedicaModel;

namespace VedicaAdmin.Controllers
{
    public class TreatmentTypesController : Controller
    {
        //
        // GET: /TreatmentTypes/
        VedicaDBEntities context = new VedicaDBEntities();

        [Authorize]
        public ActionResult Index()
        {
            var lstTreatmentTypes = context.TreatmentTypes.ToList();
            return View(lstTreatmentTypes);
        }
        [Authorize]
        public string UpdateTreatmentTypes(int id, string treatmentTypeName)
        {
            string isUpdated = string.Empty;
            try
            {
                var treamentType = context.TreatmentTypes.Where(c => c.Id == id).First();
                treamentType.Name = treatmentTypeName;
                context.Entry(treamentType).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                isUpdated = "true";
            }
            catch (Exception ex)
            {
                isUpdated = ex.Message;
            }
            return isUpdated;
        }
	}
}