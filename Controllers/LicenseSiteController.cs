using BLMS.Context;
using BLMS.Custom_Attributes;
using BLMS.CustomAttributes;
using BLMS.Enums;
using BLMS.Models;
using BLMS.Models.License;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using static BLMS.Helper;

namespace BLMS.Controllers
{
    public class LicenseSiteController : Controller
    {
        readonly LicenseDBContext licenseDbContext = new LicenseDBContext();
        readonly ddlLicenseDBContext ddlLicenseDBContext = new ddlLicenseDBContext();

        private IWebHostEnvironment _env;

        #region GRIDVIEW
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.SITE)]
        public IActionResult Index()
        {
            List<LicenseSite> licenseSiteList = licenseDbContext.LicenseSiteGetAll().ToList();

            if (TempData["registerMessage"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Success, TempData["registerMessage"].ToString());
            }
            else if (TempData["renewalMessage"] != null)
            {
                ViewBag.Alert = AlertNotification.ShowAlert(Alert.Success, TempData["renewalMessage"].ToString());
            }

            return View(licenseSiteList);
        }
        #endregion

        #region VIEW
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.SITE)]
        [NoDirectAccess]
        public ActionResult Details(int id)
        {
            LicenseSite licenseSite = licenseDbContext.GetLicenseSiteByID(id);

            if (licenseSite == null)
            {
                return NotFound();
            }

            return View(licenseSite);
        }
        #endregion

        #region REGISTER
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.SITE)]
        [HttpGet]
        public ActionResult Register()
        {
            //ddlCategory
            List<LicenseSite> categoryLicenseSiteList = ddlLicenseDBContext.ddlCategoryLicenseSite().ToList();
            categoryLicenseSiteList.Insert(0, new LicenseSite { CategoryID = 0, CategoryName = "Please select License Type" });
            ViewBag.ListofCategory = categoryLicenseSiteList;

            //ddlBusinessDiv
            List<LicenseSite> businessDivLicenseSiteList = ddlLicenseDBContext.ddlBusinessDivLicenseSite().ToList();
            businessDivLicenseSiteList.Insert(0, new LicenseSite { DivID = 0, DivName = "Please select Business Division" });
            ViewBag.ListofBusinessDiv = businessDivLicenseSiteList;

            //ddlBusinessUnit
            List<LicenseSite> businessUnitLicenseSiteList = ddlLicenseDBContext.ddlBusinessUnitLicenseSite().ToList();
            businessUnitLicenseSiteList.Insert(0, new LicenseSite { UnitID = 0, UnitName = "Please select Business Unit" });
            ViewBag.ListofBusinessUnit = businessUnitLicenseSiteList;

            //ddlPIC2
            List<LicenseSite> PIC2LicenseSiteList = ddlLicenseDBContext.ddlPIC2LicenseSite().ToList();
            PIC2LicenseSiteList.Insert(0, new LicenseSite { PIC2StaffNo = "-", PIC2Name = "Please select PIC 2 Name" });
            ViewBag.ListofPIC2 = PIC2LicenseSiteList;

            //ddlPIC3
            List<LicenseSite> PIC3LicenseSiteList = ddlLicenseDBContext.ddlPIC3LicenseSite().ToList();
            PIC3LicenseSiteList.Insert(0, new LicenseSite { PIC3StaffNo = "-", PIC3Name = "Please select PIC 3 Name" });
            ViewBag.ListofPIC3 = PIC3LicenseSiteList;

            return View();
        }

        // POST: LicenseSiteController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind] LicenseSite licenseSite, List<IFormFile> LicenseFile)
        {
            try
            {
                string UserName = HttpContext.User.Identity.Name;

                DateTime issuedDT = Convert.ToDateTime(licenseSite.IssuedDT);
                DateTime expiredDT = Convert.ToDateTime(licenseSite.ExpiredDT);

                List<LicenseSite> categoryLicenseSiteList, businessDivLicenseSiteList, businessUnitLicenseSiteList, PIC2LicenseSiteList, PIC3LicenseSiteList;

                foreach (var licenseFile in LicenseFile)
                {
                    var fileName = Path.GetFileNameWithoutExtension(licenseFile.FileName);
                    var extension = Path.GetExtension(licenseFile.FileName);

                    licenseSite.FileType = licenseFile.ContentType;
                    licenseSite.Extension = extension;
                    licenseSite.LicenseFileName = fileName;
                }

                if (string.IsNullOrEmpty(licenseSite.LicenseFileName))
                {
                    ModelState.AddModelError("", "Please upload License File");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please upload License File");
                }
                else if (string.IsNullOrEmpty(licenseSite.LicenseName))
                {
                    ModelState.AddModelError("", "Please type License Name");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type License Name");
                }
                else if (string.IsNullOrEmpty(licenseSite.RegistrationNo))
                {
                    ModelState.AddModelError("", "Please type Registration No");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please type Registration No");
                }
                else if (licenseSite.CategoryID == 0)
                {
                    ModelState.AddModelError("", "Please select License Type");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select License Type");
                }
                else if (licenseSite.DivID == 0)
                {
                    ModelState.AddModelError("", "Please select Business Division");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select Business Division");
                }
                else if (licenseSite.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please select Business Unit");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Please select Business Unit");
                }
                else if (licenseSite.IssuedDT != null && licenseSite.ExpiredDT != null && issuedDT.Date > expiredDT.Date)
                {
                    ModelState.AddModelError("", "Expire Date {1} cannot less than Issue Date {0}");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, string.Format("Expire Date {1} cannot less than Issue Date {0}", licenseSite.IssuedDT, licenseSite.ExpiredDT));
                }
                else if (string.IsNullOrEmpty(licenseSite.PIC1Name))
                {
                    ModelState.AddModelError("", "Login session is expired. Please login again");
                    ViewBag.Alert = AlertNotification.ShowAlert(Alert.Warning, "Login session is expired. Please login again");
                }
                else if (ModelState.IsValid)
                {
                    if (String.IsNullOrEmpty(licenseSite.SerialNo))
                    {
                        licenseSite.SerialNo = "-";
                    }

                    if (String.IsNullOrEmpty(licenseSite.IssuedDT))
                    {
                        licenseSite.IssuedDT = "-";
                    }

                    if (String.IsNullOrEmpty(licenseSite.ExpiredDT))
                    {
                        licenseSite.ExpiredDT = "-";
                    }

                    if (String.IsNullOrEmpty(licenseSite.Remarks))
                    {
                        licenseSite.Remarks = "-";
                    }

                    foreach (var licenseFile in LicenseFile)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(licenseFile.FileName);
                        var extension = Path.GetExtension(licenseFile.FileName);

                        licenseSite.FileType = licenseFile.ContentType;
                        licenseSite.Extension = extension;
                        licenseSite.LicenseFileName = fileName;

                        using (var dataStream = new MemoryStream())
                        {
                            licenseFile.CopyToAsync(dataStream);
                            licenseSite.Data = dataStream.ToArray();
                        }
                    }

                    if (LicenseFile.Count == 0)
                    {
                        licenseSite.LicenseFileName = "-";
                    }

                    LicenseSite checkLicenseSite = licenseDbContext.CheckLicenseSiteByName(licenseSite.LicenseName);
                    
                    if (checkLicenseSite.ExistData == 1)
                    {
                        ViewBag.Alert = AlertNotification.ShowAlert(Alert.Danger, string.Format("{0} already existed in BLMS database!", licenseSite.LicenseName));

                        //ddlCategory      
                        categoryLicenseSiteList = ddlLicenseDBContext.ddlCategoryLicenseSite().ToList();
                        categoryLicenseSiteList.Insert(0, new LicenseSite { CategoryID = 0, CategoryName = "Please select License Type" });
                        ViewBag.ListofCategory = categoryLicenseSiteList;

                        //ddlBusinessDiv      
                        businessDivLicenseSiteList = ddlLicenseDBContext.ddlBusinessDivLicenseSite().ToList();
                        businessDivLicenseSiteList.Insert(0, new LicenseSite { DivID = 0, DivName = "Please select Business Division" });
                        ViewBag.ListofBusinessDiv = businessDivLicenseSiteList;

                        //ddlBusinessUnit             
                        businessUnitLicenseSiteList = ddlLicenseDBContext.ddlBusinessUnitLicenseSite().ToList();
                        businessUnitLicenseSiteList.Insert(0, new LicenseSite { UnitID = 0, UnitName = "Please select Business Unit" });
                        ViewBag.ListofBusinessUnit = businessUnitLicenseSiteList;

                        //ddlPIC2                  
                        PIC2LicenseSiteList = ddlLicenseDBContext.ddlPIC2LicenseSite().ToList();
                        PIC2LicenseSiteList.Insert(0, new LicenseSite { PIC2StaffNo = "-", PIC2Name = "Please select PIC 2 Name" });
                        ViewBag.ListofPIC2 = PIC2LicenseSiteList;

                        //ddlPIC3   
                        PIC3LicenseSiteList = ddlLicenseDBContext.ddlPIC3LicenseSite().ToList();
                        PIC3LicenseSiteList.Insert(0, new LicenseSite { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
                        ViewBag.ListofPIC3 = PIC3LicenseSiteList;

                        return View(licenseSite);
                    }
                    else
                    {
                        string LicenseName = licenseSite.LicenseName;
                        string Issued = licenseSite.IssuedDT;
                        string Expired = licenseSite.ExpiredDT;

                        licenseDbContext.RegisterLicenseSite(licenseSite, Issued, Expired, UserName);

                        TempData["registerMessage"] = string.Format("{0} has been successfully registered!", LicenseName);

                        //ddlCategory      
                        categoryLicenseSiteList = ddlLicenseDBContext.ddlCategoryLicenseSite().ToList();
                        categoryLicenseSiteList.Insert(0, new LicenseSite { CategoryID = 0, CategoryName = "Please select License Type" });
                        ViewBag.ListofCategory = categoryLicenseSiteList;

                        //ddlBusinessDiv      
                        businessDivLicenseSiteList = ddlLicenseDBContext.ddlBusinessDivLicenseSite().ToList();
                        businessDivLicenseSiteList.Insert(0, new LicenseSite { DivID = 0, DivName = "Please select Business Division" });
                        ViewBag.ListofBusinessDiv = businessDivLicenseSiteList;

                        //ddlBusinessUnit             
                        businessUnitLicenseSiteList = ddlLicenseDBContext.ddlBusinessUnitLicenseSite().ToList();
                        businessUnitLicenseSiteList.Insert(0, new LicenseSite { UnitID = 0, UnitName = "Please select Business Unit" });
                        ViewBag.ListofBusinessUnit = businessUnitLicenseSiteList;

                        //ddlPIC2                  
                        PIC2LicenseSiteList = ddlLicenseDBContext.ddlPIC2LicenseSite().ToList();
                        PIC2LicenseSiteList.Insert(0, new LicenseSite { PIC2StaffNo = "-", PIC2Name = "Please select PIC 2 Name" });
                        ViewBag.ListofPIC2 = PIC2LicenseSiteList;

                        //ddlPIC3   
                        PIC3LicenseSiteList = ddlLicenseDBContext.ddlPIC3LicenseSite().ToList();
                        PIC3LicenseSiteList.Insert(0, new LicenseSite { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
                        ViewBag.ListofPIC3 = PIC3LicenseSiteList;

                        return RedirectToAction("Index");
                    }
                }

                //ddlCategory      
                categoryLicenseSiteList = ddlLicenseDBContext.ddlCategoryLicenseSite().ToList();
                categoryLicenseSiteList.Insert(0, new LicenseSite { CategoryID = 0, CategoryName = "Please select License Type" });
                ViewBag.ListofCategory = categoryLicenseSiteList;

                //ddlBusinessDiv      
                businessDivLicenseSiteList = ddlLicenseDBContext.ddlBusinessDivLicenseSite().ToList();
                businessDivLicenseSiteList.Insert(0, new LicenseSite { DivID = 0, DivName = "Please select Business Division" });
                ViewBag.ListofBusinessDiv = businessDivLicenseSiteList;

                //ddlBusinessUnit             
                businessUnitLicenseSiteList = ddlLicenseDBContext.ddlBusinessUnitLicenseSite().ToList();
                businessUnitLicenseSiteList.Insert(0, new LicenseSite { UnitID = 0, UnitName = "Please select Business Unit" });
                ViewBag.ListofBusinessUnit = businessUnitLicenseSiteList;

                //ddlPIC2                  
                PIC2LicenseSiteList = ddlLicenseDBContext.ddlPIC2LicenseSite().ToList();
                PIC2LicenseSiteList.Insert(0, new LicenseSite { PIC2StaffNo = "-", PIC2Name = "Please select PIC 2 Name" });
                ViewBag.ListofPIC2 = PIC2LicenseSiteList;

                //ddlPIC3   
                PIC3LicenseSiteList = ddlLicenseDBContext.ddlPIC3LicenseSite().ToList();
                PIC3LicenseSiteList.Insert(0, new LicenseSite { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
                ViewBag.ListofPIC3 = PIC3LicenseSiteList;

                return View(licenseSite);
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region RENEWAL
        // GET: CategoryController/Renewal/5
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.SITE)]
        [NoDirectAccess]
        public ActionResult Renewal(int id)
        {
            LicenseSite licenseSite = licenseDbContext.GetLicenseSiteByID(id);

            //ddlCategory
            List<LicenseSite> categoryLicenseSiteList = ddlLicenseDBContext.ddlCategoryLicenseSite().ToList();
            categoryLicenseSiteList.Insert(0, new LicenseSite { CategoryID = 0, CategoryName = "Please Select Type of License" });
            ViewBag.ListofCategory = categoryLicenseSiteList;

            //ddlBusinessDiv
            List<LicenseSite> businessDivLicenseSiteList = ddlLicenseDBContext.ddlBusinessDivLicenseSite().ToList();
            businessDivLicenseSiteList.Insert(0, new LicenseSite { DivID = 0, DivName = "Please Select Business Division" });
            ViewBag.ListofBusinessDiv = businessDivLicenseSiteList;

            //ddlBusinessUnit
            List<LicenseSite> businessUnitLicenseSiteList = ddlLicenseDBContext.ddlBusinessUnitLicenseSite().ToList();
            businessUnitLicenseSiteList.Insert(0, new LicenseSite { UnitID = 0, UnitName = "Please Select Business Unit" });
            ViewBag.ListofBusinessUnit = businessUnitLicenseSiteList;

            //ddlPIC2
            List<LicenseSite> PIC2LicenseSiteList = ddlLicenseDBContext.ddlPIC2LicenseSite().ToList();
            PIC2LicenseSiteList.Insert(0, new LicenseSite { PIC2StaffNo = "-", PIC2Name = "Please Select PIC 2 Name" });
            ViewBag.ListofPIC2 = PIC2LicenseSiteList;

            //ddlPIC3
            List<LicenseSite> PIC3LicenseSiteList = ddlLicenseDBContext.ddlPIC3LicenseSite().ToList();
            PIC3LicenseSiteList.Insert(0, new LicenseSite { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
            ViewBag.ListofPIC3 = PIC3LicenseSiteList;

            if (licenseSite == null)
            {
                return NotFound();
            }

            return View(licenseSite);
        }

        // POST: PICController/Renewal/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Renewal(int id, [Bind] LicenseSite licenseSite, List<IFormFile> RenewalLicenseFile)
        {
            try
            {
                string UserName = HttpContext.User.Identity.Name;

                List<LicenseSite> categoryLicenseSiteList, businessDivLicenseSiteList, businessUnitLicenseSiteList, PIC2LicenseSiteList, PIC3LicenseSiteList;

                foreach (var licenseFile in RenewalLicenseFile)
                {
                    var fileName = Path.GetFileNameWithoutExtension(licenseFile.FileName);
                    var extension = Path.GetExtension(licenseFile.FileName);

                    licenseSite.FileType = licenseFile.ContentType;
                    licenseSite.Extension = extension;
                    licenseSite.LicenseFileName = fileName;
                }

                if (licenseSite.CategoryID == 0)
                {
                    ModelState.AddModelError("", "Please Select Type of License");
                }
                else if (licenseSite.DivID == 0)
                {
                    ModelState.AddModelError("", "Please Select Business Division");
                }
                else if (licenseSite.UnitID == 0)
                {
                    ModelState.AddModelError("", "Please Select Business Unit");
                }
                else if (ModelState.IsValid)
                {
                    if (String.IsNullOrEmpty(licenseSite.RenewalSerialNo))
                    {
                        licenseSite.RenewalSerialNo = "-";
                    }

                    if (String.IsNullOrEmpty(licenseSite.RenewalIssuedDT))
                    {
                        licenseSite.RenewalIssuedDT = "-";
                    }

                    if (String.IsNullOrEmpty(licenseSite.RenewalExpiredDT))
                    {
                        licenseSite.RenewalExpiredDT = "-";
                    }

                    if (String.IsNullOrEmpty(licenseSite.Remarks))
                    {
                        licenseSite.Remarks = "-";
                    }

                    foreach (var renewalLicenseFile in RenewalLicenseFile)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(renewalLicenseFile.FileName);
                        var extension = Path.GetExtension(renewalLicenseFile.FileName);

                        licenseSite.RenewalFileType = renewalLicenseFile.ContentType;
                        licenseSite.RenewalExtension = extension;
                        licenseSite.RenewalLicenseFileName = fileName;

                        using (var dataStream = new MemoryStream())
                        {
                            renewalLicenseFile.CopyToAsync(dataStream);
                            licenseSite.RenewalData = dataStream.ToArray();
                        }
                    }

                    if (RenewalLicenseFile.Count == 0)
                    {
                        licenseSite.RenewalLicenseFileName = "-";
                    }

                    licenseDbContext.RenewalLicenseSite(licenseSite, UserName);

                    //ddlCategory      
                    categoryLicenseSiteList = ddlLicenseDBContext.ddlCategoryLicenseSite().ToList();
                    categoryLicenseSiteList.Insert(0, new LicenseSite { CategoryID = 0, CategoryName = "Please select License Type" });
                    ViewBag.ListofCategory = categoryLicenseSiteList;

                    //ddlBusinessDiv      
                    businessDivLicenseSiteList = ddlLicenseDBContext.ddlBusinessDivLicenseSite().ToList();
                    businessDivLicenseSiteList.Insert(0, new LicenseSite { DivID = 0, DivName = "Please select Business Division" });
                    ViewBag.ListofBusinessDiv = businessDivLicenseSiteList;

                    //ddlBusinessUnit             
                    businessUnitLicenseSiteList = ddlLicenseDBContext.ddlBusinessUnitLicenseSite().ToList();
                    businessUnitLicenseSiteList.Insert(0, new LicenseSite { UnitID = 0, UnitName = "Please select Business Unit" });
                    ViewBag.ListofBusinessUnit = businessUnitLicenseSiteList;

                    //ddlPIC2                  
                    PIC2LicenseSiteList = ddlLicenseDBContext.ddlPIC2LicenseSite().ToList();
                    PIC2LicenseSiteList.Insert(0, new LicenseSite { PIC2StaffNo = "-", PIC2Name = "Please select PIC 2 Name" });
                    ViewBag.ListofPIC2 = PIC2LicenseSiteList;

                    //ddlPIC3   
                    PIC3LicenseSiteList = ddlLicenseDBContext.ddlPIC3LicenseSite().ToList();
                    PIC3LicenseSiteList.Insert(0, new LicenseSite { PIC3StaffNo = "-", PIC3Name = "Please Select PIC 3 Name" });
                    ViewBag.ListofPIC3 = PIC3LicenseSiteList;

                    return RedirectToAction("Index");
                }

                return View(licenseSite);
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region EMAIL SERVICE

        #endregion

        #region DOWNLOAD FILE
        // GET: LicenseHQController/Download
        public ActionResult DownloadLicenseFile(int id)
        {
            LicenseSite licenseSite = licenseDbContext.GetLicenseSiteByID(id);

            if (licenseSite == null)
            {
                return NotFound();
            }

            return File(licenseSite.Data, licenseSite.FileType, licenseSite.LicenseFileName + licenseSite.Extension);
        }
        #endregion

        #region GET LINKED BUSINESS UNIT
        public JsonResult JSONGetUnitName(int DivID)
        {
            DataSet ds = ddlLicenseDBContext.ddlBusinessUnitLinkedDivSite(DivID);
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
