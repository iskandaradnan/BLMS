using BLMS.Context;
using BLMS.Custom_Attributes;
using BLMS.CustomAttributes;
using BLMS.Enums;
using BLMS.Models.Admin;
using BLMS.v2.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BLMS.Helper;

namespace BLMS.Controllers
{
    public class BusinessDivController : Controller
    {
        readonly AdminDBContext dbContext = new AdminDBContext();
        readonly ddlAdminDBContext ddlDBContext = new ddlAdminDBContext();
        readonly AuditLogDbContext logController = new AuditLogDbContext();

        #region GRIDVIEW
        [Authorize(Roles.ADMINISTRATOR)]
        [Authorize(AccessLevel.ADMINISTRATION)]
        public ActionResult Index()
        {
            List<BusinessDiv> BusinessDivList = dbContext.BusinessDivGetAll().ToList();

            if (TempData["createMessage"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Success, TempData["createMessage"].ToString());
            }
            else if (TempData["editMessage"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Success, TempData["editMessage"].ToString());
            }
            else if (TempData["deleteMessage"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Delete, TempData["deleteMessage"].ToString());
            }
            else if (TempData["deleteMessageError"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, TempData["deleteMessageError"].ToString());
            } 

            return View(BusinessDivList);
        }
        #endregion

        #region CREATE
        [Authorize(Roles.ADMINISTRATOR)]
        [Authorize(AccessLevel.ADMINISTRATION)]
        [NoDirectAccess]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessDivController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] BusinessDiv businessDiv)
        {
            string UserName = HttpContext.User.Identity.Name;

            try
            {
                
                if (string.IsNullOrEmpty(businessDiv.DivName))
                {
                    ModelState.AddModelError("", "Please type Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type Business Division");
                }
                else if (ModelState.IsValid)
                {
                    string DivName = businessDiv.DivName;

                    BusinessDiv checkbusinessDiv = dbContext.CheckBusinessDivByName(DivName);

                    if (checkbusinessDiv.ExistData == 1)
                    {
                        ViewBag.Alert = AlertNotification.ShowAlert(Alert.Danger, string.Format("{0} already existed in BLMS database!", DivName));
                        return View(businessDiv);
                    }
                    else
                    {
                        dbContext.AddBusinessDiv(businessDiv, UserName);
                        TempData["createMessage"] = string.Format("{0} has been successfully created!", DivName);
                        return RedirectToAction("Index");
                    }
                }

                return View(businessDiv);
            }
            catch(Exception ex)
            {

                string path = "BUSINESS DIVISION";

                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

                string msg = ex.Message;
                string method = trace.GetFrame((trace.FrameCount - 1)).GetMethod().ToString();
                Int32 lineNumber = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber();

                logController.AddErrorLog(path, method, lineNumber, msg, UserName);


                return View();
            }
        }
        #endregion

        #region EDIT
        [Authorize(Roles.ADMINISTRATOR)]
        [Authorize(AccessLevel.ADMINISTRATION)]
        [NoDirectAccess]
        public ActionResult Edit(int id)
        {
            BusinessDiv businessDiv = dbContext.GetBusinessDivByID(id);

            if (businessDiv == null)
            {
                return NotFound();
            }

            return View(businessDiv);
        }

        // POST: BusinessDivController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] BusinessDiv businessDiv)
        {
            string UserName = HttpContext.User.Identity.Name;

            try
            {


                if (string.IsNullOrEmpty(businessDiv.DivName))
                {
                    ModelState.AddModelError("", "Please type Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type Business Division");
                }
                else if (ModelState.IsValid)
                {
                    string DivName = businessDiv.DivName;
                    string OldDivName = businessDiv.OldDivName;

                    BusinessDiv checkbusinessDiv = dbContext.CheckBusinessDivByName(DivName);

                    if (checkbusinessDiv.ExistData == 1 && DivName != OldDivName)
                    {
                        ViewBag.Alert = AlertNotification.ShowAlert(Alert.Danger, string.Format("{0} already existed in BLMS database!", businessDiv.DivName));
                        return View(businessDiv);
                    }
                    else if (checkbusinessDiv.ExistData == 1 && DivName == OldDivName)
                    {
                        dbContext.EditBusinessDiv(businessDiv, UserName);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        dbContext.EditBusinessDiv(businessDiv, UserName);
                        TempData["editMessage"] = string.Format("{0} has been successfully edited!", businessDiv.DivName);
                        return RedirectToAction("Index");
                    }
                }

                return View(dbContext);
            }
            catch(Exception ex)
            {
                string path = "BUSINESS DIVISION";

                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

                string msg = ex.Message;
                string method = trace.GetFrame((trace.FrameCount - 1)).GetMethod().ToString();
                Int32 lineNumber = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber();

                logController.AddErrorLog(path, method, lineNumber, msg, UserName);


                return View();
            }
        }
        #endregion

        #region DELETE
        [Authorize(Roles.ADMINISTRATOR)]
        [Authorize(AccessLevel.ADMINISTRATION)]
        public JsonResult Delete(int Id)
        {
            string UserName = HttpContext.User.Identity.Name;

            try
            {
                BusinessDiv businessDiv = dbContext.GetBusinessDivByID(Id);
                BusinessDiv CheckLinkedbusinessDiv = dbContext.CheckLinkedBusinessUnitByName(businessDiv.DivName);

                if (CheckLinkedbusinessDiv.LinkedData == 1)
                {
                    TempData["deleteMessageError"] = string.Format("{0} linked to Business Unit. Delete operation is not allowed!", businessDiv.DivName);
                }
                else
                {

                    dbContext.DeleteBusinessDiv(Id, businessDiv.DivName, UserName);
                    TempData["deleteMessage"] = string.Format("{0} has been deleted from BLMS database!", businessDiv.DivName);
                }

                return Json(new { status = "Success" });
            }
            catch(Exception ex)
            {

                string path = "BUSINESS DIVISION";

                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

                string msg = ex.Message;
                string method = trace.GetFrame((trace.FrameCount - 1)).GetMethod().ToString();
                Int32 lineNumber = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber();

                logController.AddErrorLog(path, method, lineNumber, msg, UserName);

                return Json(new { status = "Fail" }); 
            }
        }
        #endregion
    }
}
