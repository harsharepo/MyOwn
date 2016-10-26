using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VedicaModel;

namespace VedicaAdmin.Controllers
{
    public class OffersController : Controller
    {
        //
        // GET: /Offers/
        VedicaDBEntities context = new VedicaDBEntities();
        public ActionResult Index()
        {
            var offersList = context.Offers.ToList();
            return View(offersList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Offers offer)
        {
            context.Offers.Add(offer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var offerDetails = context.Offers.Where(c => c.Id == id).FirstOrDefault();
            return View(offerDetails);
        }
        [HttpPost]
        public ActionResult Edit(Offers offer)
        {
            return RedirectToAction("Index");
        }
        public string DeleteOffer(int id)
        {
            var result = string.Empty;
            var offerDetail = context.Offers.Where(c => c.Id == id).FirstOrDefault();
            context.Offers.Remove(offerDetail);
            context.SaveChanges();
            result = "true";
            return result;
        }
	}
}