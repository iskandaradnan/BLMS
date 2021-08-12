using BLMS.Context;
using BLMS.Custom_Attributes;
using BLMS.CustomAttributes;
using BLMS.Enums;
using BLMS.Models;
using BLMS.Models.License;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using static BLMS.Helper;

namespace BLMS.Controllers
{
    public class LicenseHQController : Controller
    {
        readonly LicenseDBContext licenseDbContext = new LicenseDBContext();
        readonly ddlLicenseDBContext ddlLicenseDBContext = new ddlLicenseDBContext();

        private IWebHostEnvironment _env;

        #region GRIDVIEW
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.HQ)]
        public IActionResult Index()
        {
            List<LicenseHQ> licenseHQList = licenseDbContext.LicenseHQGetAll().ToList();

            if (TempData["requestLicenseMessage"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Success, TempData["requestLicenseMessage"].ToString());
            }
            else if (TempData["requestRenewalMessage"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Success, TempData["requestRenewalMessage"].ToString());
            }

            return View(licenseHQList);
        }
        #endregion

        #region VIEW REQUEST LICENSE
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.HQ)]
        [NoDirectAccess]
        public ActionResult DetailRequest(int id)
        {
            LicenseHQ licenseHQ = licenseDbContext.GetLicenseHQByID(id);

            if (licenseHQ == null)
            {
                return NotFound();
            }

            return View(licenseHQ);
        }
        #endregion

        #region VIEW REGISTER LICENSE
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.HQ)]
        [NoDirectAccess]
        public ActionResult DetailRegister(int id)
        {
            LicenseHQ licenseHQ = licenseDbContext.GetLicenseHQByID(id);

            if (licenseHQ == null)
            {
                return NotFound();
            }

            return View(licenseHQ);
        }
        #endregion

        #region REQUEST
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.HQ)]
        public ActionResult RequestLicense()
        {
            //ddlCategory
            List<LicenseHQ> categoryLicenseHQList = ddlLicenseDBContext.ddlCategoryLicenseHQ().ToList();
            categoryLicenseHQList.Insert(0, new LicenseHQ { CategoryID = 0, CategoryName = "Please Select Type of License" });
            ViewBag.ListofCategory = categoryLicenseHQList;

            //ddlBusinessDiv
            List<LicenseHQ> businessDivLicenseHQList = ddlLicenseDBContext.ddlBusinessDivLicenseHQ().ToList();
            businessDivLicenseHQList.Insert(0, new LicenseHQ { DivID = 0, DivName = "Please Select Business Division" });
            ViewBag.ListofBusinessDiv = businessDivLicenseHQList;

            //ddlBusinessUnit
            List<LicenseHQ> businessUnitLicenseHQList = ddlLicenseDBContext.ddlBusinessUnitLicenseHQ().ToList();
            businessUnitLicenseHQList.Insert(0, new LicenseHQ { UnitID = 0, UnitName = "Please Select Business Unit" });
            ViewBag.ListofBusinessUnit = businessUnitLicenseHQList;

            //ddlPIC2
            List<LicenseHQ> PIC2LicenseHQList = ddlLicenseDBContext.ddlPIC2LicenseHQ().ToList();
            PIC2LicenseHQList.Insert(0, new LicenseHQ { PIC2StaffNo = "-", PIC2Name = "Please Select PIC 2 Name" });
            ViewBag.ListofPIC2 = PIC2LicenseHQList;

            //ddlPIC3
            List<LicenseHQ> PIC3LicenseHQList = ddlLicenseDBContext.ddlPIC3LicenseHQ().ToList();
            PIC3LicenseHQList.Insert(0, new LicenseHQ { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
            ViewBag.ListofPIC3 = PIC3LicenseHQList;

            return View();
        }

        // POST: LicenseHQController/Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestLicense([Bind] LicenseHQ licenseHQ)
        {
            try
            {
                string UserName = User.Identity.Name;

                List<LicenseHQ> categoryLicenseHQList, businessDivLicenseHQList, businessUnitLicenseHQList, PIC2LicenseHQList, PIC3LicenseHQList;

                if (string.IsNullOrEmpty(licenseHQ.PIC1Name))
                {
                    ModelState.AddModelError("", "Your session has been expired. Please login again.");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Your session has been expired. Please login again.");
                }
                else if (string.IsNullOrEmpty(licenseHQ.LicenseName) && licenseHQ.CategoryID == 0 && licenseHQ.DivID == 0 && licenseHQ.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ModelState.AddModelError("", "Please select License Type");
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningFour, "Please type License Name", "Please select License Type", "Please select Business Division", "Please select Business Unit");
                }
                else if (string.IsNullOrEmpty(licenseHQ.LicenseName) && licenseHQ.CategoryID == 0 && licenseHQ.DivID == 0)
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ModelState.AddModelError("", "Please select License Type");
                    ModelState.AddModelError("", "Please select Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningThree, "Please type License Name", "Please select License Type", "Please select Business Division", "");
                }
                else if (string.IsNullOrEmpty(licenseHQ.LicenseName) && licenseHQ.CategoryID == 0 && licenseHQ.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ModelState.AddModelError("", "Please select License Type");
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningThree, "Please type License Name", "Please select License Type", "Please select Business Unit", "");
                }
                else if (licenseHQ.CategoryID == 0 && licenseHQ.DivID == 0 && licenseHQ.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please select License Type");
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningThree, "Please select License Type", "Please select Business Division", "Please select Business Unit", "");
                }
                else if (string.IsNullOrEmpty(licenseHQ.LicenseName) && licenseHQ.CategoryID == 0)
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ModelState.AddModelError("", "Please select License Type");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please type License Name", "Please select License Type", "", "");
                }
                else if (string.IsNullOrEmpty(licenseHQ.LicenseName) && licenseHQ.DivID == 0)
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ModelState.AddModelError("", "Please select Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please type License Name", "Please select Business Division", "", "");
                }
                else if (string.IsNullOrEmpty(licenseHQ.LicenseName) && licenseHQ.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please type License Name", "Please select Business Unit", "", "");
                }
                else if (licenseHQ.CategoryID == 0 && licenseHQ.DivID == 0)
                {
                    ModelState.AddModelError("", "Please select License Type");
                    ModelState.AddModelError("", "Please select Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please select License Type", "Please select Business Division", "", "");
                }
                else if (licenseHQ.CategoryID == 0 && licenseHQ.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please select License Type");
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please select License Type", "Please select Business Unit", "", "");
                }
                else if (licenseHQ.DivID == 0 && licenseHQ.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlertAll(Alert.WarningTwo, "Please select Business Division", "Please select Business Unit", "", "");
                }
                else if (string.IsNullOrEmpty(licenseHQ.LicenseName))
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type License Name");
                }
                else if (licenseHQ.CategoryID == 0)
                {
                    ModelState.AddModelError("", "Please select License Type");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select License Type");
                }
                else if (licenseHQ.DivID == 0)
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select Business Division");
                }
                else if (licenseHQ.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select Business Unit");
                }
                else if (ModelState.IsValid)
                {
                    if (String.IsNullOrEmpty(licenseHQ.Remarks))
                    {
                        licenseHQ.Remarks = "-";
                    }

                    LicenseHQ checkLicense = licenseDbContext.CheckLicenseByName(licenseHQ.LicenseName);

                    if (checkLicense.ExistData == 1)
                    {
                        ViewBag.Alert = AlertNotification.ShowAlert(Alert.Danger, string.Format("{0} already existed in BLMS database!", licenseHQ.LicenseName));

                        //ddlCategory
                        categoryLicenseHQList = ddlLicenseDBContext.ddlCategoryLicenseHQ().ToList();
                        categoryLicenseHQList.Insert(0, new LicenseHQ { CategoryID = 0, CategoryName = "Please Select Type of License" });
                        ViewBag.ListofCategory = categoryLicenseHQList;

                        //ddlBusinessDiv
                        businessDivLicenseHQList = ddlLicenseDBContext.ddlBusinessDivLicenseHQ().ToList();
                        businessDivLicenseHQList.Insert(0, new LicenseHQ { DivID = 0, DivName = "Please Select Business Division" });
                        ViewBag.ListofBusinessDiv = businessDivLicenseHQList;

                        //ddlBusinessUnit
                        businessUnitLicenseHQList = ddlLicenseDBContext.ddlBusinessUnitLicenseHQ().ToList();
                        businessUnitLicenseHQList.Insert(0, new LicenseHQ { UnitID = 0, UnitName = "Please Select Business Unit" });
                        ViewBag.ListofBusinessUnit = businessUnitLicenseHQList;

                        //ddlPIC2
                        PIC2LicenseHQList = ddlLicenseDBContext.ddlPIC2LicenseHQ().ToList();
                        PIC2LicenseHQList.Insert(0, new LicenseHQ { PIC2StaffNo = "-", PIC2Name = "Please Select PIC 2 Name" });
                        ViewBag.ListofPIC2 = PIC2LicenseHQList;

                        //ddlPIC3
                        PIC3LicenseHQList = ddlLicenseDBContext.ddlPIC3LicenseHQ().ToList();
                        PIC3LicenseHQList.Insert(0, new LicenseHQ { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
                        ViewBag.ListofPIC3 = PIC3LicenseHQList;

                        return View(licenseHQ);
                    }
                    else
                    {
                        licenseDbContext.RequestLicenseHQ(licenseHQ, UserName);

                        TempData["requestLicenseMessage"] = string.Format("{0} has been successfully requested!", licenseHQ.LicenseName);
                        return RedirectToAction("Index");
                    }
                }

                //ddlCategory
                categoryLicenseHQList = ddlLicenseDBContext.ddlCategoryLicenseHQ().ToList();
                categoryLicenseHQList.Insert(0, new LicenseHQ { CategoryID = 0, CategoryName = "Please Select Type of License" });
                ViewBag.ListofCategory = categoryLicenseHQList;

                //ddlBusinessDiv
                businessDivLicenseHQList = ddlLicenseDBContext.ddlBusinessDivLicenseHQ().ToList();
                businessDivLicenseHQList.Insert(0, new LicenseHQ { DivID = 0, DivName = "Please Select Business Division" });
                ViewBag.ListofBusinessDiv = businessDivLicenseHQList;

                //ddlBusinessUnit
                businessUnitLicenseHQList = ddlLicenseDBContext.ddlBusinessUnitLicenseHQ().ToList();
                businessUnitLicenseHQList.Insert(0, new LicenseHQ { UnitID = 0, UnitName = "Please Select Business Unit" });
                ViewBag.ListofBusinessUnit = businessUnitLicenseHQList;

                //ddlPIC2
                PIC2LicenseHQList = ddlLicenseDBContext.ddlPIC2LicenseHQ().ToList();
                PIC2LicenseHQList.Insert(0, new LicenseHQ { PIC2StaffNo = "-", PIC2Name = "Please Select PIC 2 Name" });
                ViewBag.ListofPIC2 = PIC2LicenseHQList;

                //ddlPIC3
                PIC3LicenseHQList = ddlLicenseDBContext.ddlPIC3LicenseHQ().ToList();
                PIC3LicenseHQList.Insert(0, new LicenseHQ { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
                ViewBag.ListofPIC3 = PIC3LicenseHQList;

                return View(licenseHQ);
            }
            catch
            {
                TempData["requestLicenseMessage"] = string.Format("{0} has been successfully requested!", licenseHQ.LicenseName);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Email
        public void Email(LicenseHQ licenseHQ)
        {
            string sendTo = "send ke siapa?";

            var webRoot = _env.WebRootPath; //get wwwroot Folder

            //Get TemplateFile located at wwwroot/Templates/EmailTemplate/Register_EmailTemplate.html
            var pathToFile = _env.WebRootPath
                    + Path.DirectorySeparatorChar.ToString()
                    + "Templates"
                    + Path.DirectorySeparatorChar.ToString()
                    + "Email"
                    + Path.DirectorySeparatorChar.ToString()
                    + "License_Request_Summary.html";

            var Title = "LICENSE REQUEST SUMMARY";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }
            //{0} : Subject
            //{1} : Email To
            //{2} : Body

            string messageBody = string.Format(builder.HtmlBody,
                Title,
                licenseHQ.LicenseName,
                licenseHQ.PIC1Name
                );

            //using (MailMessage mm = new MailMessage())
            //{
            //    mm.From = new MailAddress("noreply@blms.uemedgenta.com", "UEM Edgenta Berhad - Business License Management System");
            //    mm.To.Add(sendTo);
            //    mm.Subject = "[REQUEST] BLMS - NEW REQUEST FOR LICENSE REGISTRATION";
            //    mm.Body = messageBody;

            //    mm.IsBodyHtml = true;
            //    using (SmtpClient smtp = new SmtpClient())
            //    {
            //        smtp.Port = 25;
            //        smtp.Host = "smtp2.edgenta.com";
            //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //        smtp.UseDefaultCredentials = false;
            //        smtp.Credentials = new System.Net.NetworkCredential("user", "Password");
            //        smtp.Send(mm);
            //        ViewBag.Message = "Email sent.";
            //    }
            //}
        }
        #endregion

        #region LINKED BUSINESS UNIT TO BUSINESS DIV
        public JsonResult JSONGetUnitName(int DivID)
        {
            DataSet ds = ddlLicenseDBContext.ddlBusinessUnitLinkedDivHQ(DivID);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new SelectListItem { Text = dr["UnitName"].ToString(), Value = dr["UnitID"].ToString() });
            }
            return Json(list);
        }
        #endregion
    }
}
