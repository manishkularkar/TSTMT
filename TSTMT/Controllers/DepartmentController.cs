using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSTMT.Models;

namespace TSTMT.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DepartIndex()
        {
            return View();
        }
        // Save
        public ActionResult SaveDepart(DepartModel model)
        {
            try
            {
                return Json(new { Message = new DepartModel().SaveDepart(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // List View // 
        public ActionResult DepartmentList()
        {
            try
            {
                return Json(new { model = (new DepartModel().DepartmentList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // delete
        public ActionResult DeleteDepartment(int Department_id)
        {
            try
            {
                return Json(new { model = (new DepartModel().DeleteDepartment(Department_id)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Edit
        public ActionResult EditDepartment(int Department_id)
        {
            try
            {
                return Json(new { model = new DepartModel().EditDepartment(Department_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Detail
        public ActionResult DetailDepartment(int Department_id)
        {
            try
            {
                return Json(new { model = new DepartModel().DetailDepartment(Department_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}