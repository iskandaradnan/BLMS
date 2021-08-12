using BLMS.Context;
using BLMS.Custom_Attributes;
using BLMS.CustomAttributes;
using BLMS.Enums;
using BLMS.Models.License;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Controllers
{
    public class LicenseAdminController : Controller
    {
        readonly LicenseDBContext licenseDbContext = new LicenseDBContext();
        readonly ddlLicenseDBContext ddlLicenseDBContext = new ddlLicenseDBContext();

        private IWebHostEnvironment _env;

        #region GRIDVIEW
        [Authorize(Roles.ADMINISTRATOR, Roles.PIC)]
        [Authorize(AccessLevel.ADMINISTRATION, AccessLevel.HQ)]
        public IActionResult Index()
        {
            List<LicenseAdmin> licenseHQList = licenseDbContext.LicenseAdminGetAll().ToList();

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
    }
}
