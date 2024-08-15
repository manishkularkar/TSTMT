using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSTMT.Models;

namespace TSTMT.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TransIndex()
        {
            return View();
        }
        // Department ddl fetch
        public ActionResult ddlDepartment()
        {
            try
            {
                return Json(new { model = (new TransModel().ddlDepartment()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        // Vendor ddl fetch
        public ActionResult ddlVendor()
        {
            try
            {
                return Json(new { model = (new TransModel().ddlVendor()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Ddl Item id n name fetch
        public ActionResult ddlItemId()
        {

            try
            {
                return Json(new { model = (new TransModel().ddlItemId()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        // ddl item qty fhetch
        public ActionResult ddlItemQty(int Item_id)
        {

            try
            {
                return Json(new { model = (new TransModel().ddlItemQty(Item_id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // save n update

        public ActionResult SaveTrans(TransModel model)
        {
            try
            {

                return Json(new { Message = new TransModel().SaveTrans(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // List View // 
        public ActionResult TransactionList()
        {
            try
            {
                return Json(new { model = (new TransModel().TransactionList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // delete
        public ActionResult DeleteTransaction(int Transaction_id)
        {
            try
            {
                return Json(new { model = (new TransModel().DeleteTransaction(Transaction_id)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}