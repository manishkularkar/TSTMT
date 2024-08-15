using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSTMT.Models;

namespace TSTMT.Controllers
{
    public class ItemController : Controller
    {

        // Genrate serial no.
       // private int serialCounter = 1;

        // GET: Item
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ItemIndex()
        {
            return View();
        }


        // Save
        public ActionResult SaveItem(ItemModel model)
        {
            try
            {
                return Json(new { Message = new ItemModel().SaveItem(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
        // Edit
         public ActionResult EditItem(int Item_id)
        {
            try 
            {
                return Json(new { model = new ItemModel().EditItem(Item_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // List View // 

        public ActionResult ItemList()
        {
            try
            {
                return Json(new { model = (new ItemModel().ItemList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // delete
        public ActionResult DeleteItem(int Item_id)
        {
            try
            {
                return Json(new { model = (new ItemModel().DeleteItem(Item_id)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // Detail
        public ActionResult DetailItem(int Item_id)
        {
            try
            {
                return Json(new { model = new ItemModel().DetailItem(Item_id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        // Drop Down bind Category
        public ActionResult ddlCategoryList()
        {

            try
            {
                return Json(new { model = (new ItemModel().ddlCategoryList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}





  

 