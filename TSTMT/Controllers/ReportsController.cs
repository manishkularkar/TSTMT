using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSTMT.Models;

namespace TSTMT.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReportsIndex()
        {
            return View();
        }
        public ActionResult GetAllData()
        {
            try
            {
                var combinedDataList = new ReportsModel().GetAllData();
                return Json(new { model = combinedDataList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                // Log.Error("Error fetching combined data", ex);
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}