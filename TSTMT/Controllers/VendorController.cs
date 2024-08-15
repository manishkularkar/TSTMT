using System;
using System.Web.Mvc;
using TSTMT.Models;


namespace TSTMT.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VendorIndex()
        {
            return View();
        }
        // Save
        public ActionResult SaveVendor(VendorModel model)

        {
            try
            {
                return Json(new { Message = new VendorModel().SaveVendor(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // List View // 
        public ActionResult VendorList()
        {
            try
            {
                return Json(new { model = (new VendorModel().VendorList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // delete
        public ActionResult DeleteVendor(int Vendor_id)
        {
            try
            {
                return Json(new { model = (new VendorModel().DeleteVendor(Vendor_id)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Detail
        public ActionResult DetailVendor(int Vendor_id)
        {
            try
            {
                return Json(new { model = new VendorModel().DetailVendor(Vendor_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       

        // Edit
        public ActionResult EditVendor(int Vendor_id)
        {
            try
            {
                return Json(new { model = new VendorModel().EditVendor(Vendor_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}