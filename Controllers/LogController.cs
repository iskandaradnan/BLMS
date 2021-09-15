using BLMS.Models.Admin;
using BLMS.v2.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Controllers
{
    public class LogController : Controller
    {
        readonly AuditLogDbContext dbContext = new AuditLogDbContext();

        public IActionResult Index()
        {

            AuditErrorViewModel aevm = new AuditErrorViewModel();

            aevm.auditLog = dbContext.GetAuditLog().ToList();
            aevm.errorLog = dbContext.GetErrorLog().ToList();

            return View(aevm);          
        }

    }
}
