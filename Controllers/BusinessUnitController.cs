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
    public class BusinessUnitController : Controller
    {
        readonly AdminDBContext dbContext = new AdminDBContext();
        readonly ddlAdminDBContext ddlDBContext = new ddlAdminDBContext();
        readonly AuditLogDbContext logController = new AuditLogDbContext();

        #region GRIDVIEW
        [Authorize(Roles.ADMINISTRATOR)]
        [Authorize(AccessLevel.ADMINISTRATION)]
        public ActionResult Index()
        {
            List<BusinessUnit> BusinessUnitList = dbContext.BusinessUnitGetAll().ToList();

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

            return View(BusinessUnitList);
        }
        #endregion

        #region CREATE
        [Authorize(Roles.ADMINISTRATOR)]
        [Authorize(AccessLevel.ADMINISTRATION)]
        [NoDirectAccess]
        public ActionResult Create()
        {
            List<BusinessUnit> businessDivList = ddlDBContext.ddlBusinessDiv().ToList();
            businessDivList.Insert(0, new BusinessUnit { DivID = 0, DivName = "Please select Business Division" });
            ViewBag.ListofBusinessDiv = businessDivList;

            return View();
        }

        // POST: BusinessUnitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] BusinessUnit businessUnit, string UserName)
        {
            try
            {
                
                string UnitName = businessUnit.UnitName;
                UserName = HttpContext.User.Identity.Name;
                List<BusinessUnit> businessDivList;

                if (businessUnit.DivID == 0 && string.IsNullOrEmpty(UnitName) && string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please type Business Unit");
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningThree, "Please select Business Division", "Please type Business Unit", "Please type Head of Company", "");
                }
                else if (businessUnit.DivID == 0 && string.IsNullOrEmpty(UnitName))
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please type Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please select Business Division", "Please type Business Unit", "", "");
                }
                else if (string.IsNullOrEmpty(UnitName) && string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please type Business Unit");
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please type Business Unit", "Please type Head of Company", "", "");
                }
                else if (businessUnit.DivID == 0 && string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please select Business Division", "Please type Head of Company", "", "");
                }
                else if (businessUnit.DivID == 0)
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select Business Division");
                }
                else if (string.IsNullOrEmpty(UnitName))
                {
                    ModelState.AddModelError("", "Please type Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type Business Unit");
                }
                else if (string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type Head of Company");
                }
                else if (ModelState.IsValid)
                {
                    BusinessUnit checkbusinessUnit = dbContext.CheckBusinessUnitByName(UnitName);

                    if (checkbusinessUnit.ExistData == 1)
                    {
                        ViewBag.Alert = AlertNotification.ShowAlert(Alert.Danger, string.Format("{0} already existed in BLMS database!", UnitName));

                        businessDivList = ddlDBContext.ddlBusinessDiv().ToList();
                        businessDivList.Insert(0, new BusinessUnit { DivID = 0, DivName = "Please select Business Division" });
                        ViewBag.ListofBusinessDiv = businessDivList;

                        return View(businessUnit);
                    }
                    else
                    {
                        dbContext.AddBusinessUnit(businessUnit, UserName);
                        TempData["createMessage"] = string.Format("{0} has been successfully created!", UnitName);
                        return RedirectToAction("Index");
                    }
                }

                //Set Data Back After Postback
                businessDivList = ddlDBContext.ddlBusinessDiv().ToList();
                businessDivList.Insert(0, new BusinessUnit { DivID = 0, DivName = "Please select Business Division" });
                ViewBag.ListofBusinessDiv = businessDivList;

                return View(businessUnit);
            }
            catch(Exception ex)
            {
                string path = "BUSINESS UNIT";

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
            BusinessUnit businessUnit = dbContext.GetBusinessUnitByID(id);

            List<BusinessUnit> businessDivList = ddlDBContext.ddlBusinessDiv().ToList();
            businessDivList.Insert(0, new BusinessUnit { DivID = 0, DivName = "PLEASE SELECT BUSINESS DIVISION" });
            ViewBag.ListofBusinessDiv = businessDivList;

            if (businessUnit == null)
            {
                return NotFound();
            }

            return View(businessUnit);
        }

        // POST: BusinessUnitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind] BusinessUnit businessUnit, string UserName)
        {
            try
            {
                string UnitName = businessUnit.UnitName;
                UserName = HttpContext.User.Identity.Name;
                List<BusinessUnit> businessDivList;

                if (businessUnit.DivID == 0 && string.IsNullOrEmpty(UnitName) && string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please type Business Unit");
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningThree, "Please select Business Division", "Please type Business Unit", "Please type Head of Company", "");
                }
                else if (businessUnit.DivID == 0 && string.IsNullOrEmpty(UnitName))
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please type Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please select Business Division", "Please type Business Unit", "", "");
                }
                else if (string.IsNullOrEmpty(UnitName) && string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please type Business Unit");
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please type Business Unit", "Please type Head of Company", "", "");
                }
                else if (businessUnit.DivID == 0 && string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please select Business Division", "Please type Head of Company", "", "");
                }
                else if (businessUnit.DivID == 0)
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select Business Division");
                }
                else if (string.IsNullOrEmpty(UnitName))
                {
                    ModelState.AddModelError("", "Please type Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type Business Unit");
                }
                else if (string.IsNullOrEmpty(businessUnit.HoCName))
                {
                    ModelState.AddModelError("", "Please type Head of Company");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type Head of Company");
                }
                else if (ModelState.IsValid)
                {
                    string OldUnitName = businessUnit.OldUnitName;

                    BusinessUnit checkbusinessUnit = dbContext.CheckBusinessUnitByName(UnitName);

                    if (checkbusinessUnit.ExistData == 1 && UnitName != OldUnitName)
                    {
                        ViewBag.Alert = AlertNotification.ShowAlert(Alert.Danger, string.Format("{0} already existed in BLMS database!", UnitName));

                        businessDivList = ddlDBContext.ddlBusinessDiv().ToList();
                        businessDivList.Insert(0, new BusinessUnit { DivID = 0, DivName = "Please select Business Division" });
                        ViewBag.ListofBusinessDiv = businessDivList;

                        return View(businessUnit);
                    }
                    else if (checkbusinessUnit.ExistData == 1 && UnitName == OldUnitName)
                    {
                        dbContext.EditBusinessUnit(businessUnit, UserName);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        dbContext.EditBusinessUnit(businessUnit, UserName);
                        TempData["editMessage"] = string.Format("{0} has been successfully edited!", businessUnit.UnitName);
                        return RedirectToAction("Index");
                    }
                }

                businessDivList = ddlDBContext.ddlBusinessDiv().ToList();
                businessDivList.Insert(0, new BusinessUnit { DivID = 0, DivName = "Please select Business Division" });
                ViewBag.ListofBusinessDiv = businessDivList;

                return View(dbContext);
            }
            catch(Exception ex)
            {
                string path = "BUSINESS UNIT";

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
            string UserName = User.Identity.Name;

            try
            {
                BusinessUnit businessUnit = dbContext.GetBusinessUnitByID(Id);

                dbContext.DeleteBusinessUnit(Id, businessUnit.UnitName, UserName);
                TempData["deleteMessage"] = string.Format("{0} has been successfully deleted!", businessUnit.UnitName);

                return Json(new { status = "Success" });
            }
            catch(Exception ex)
            {
                string path = "BUSINESS UNIT";

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
