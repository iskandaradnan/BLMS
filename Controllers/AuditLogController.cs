using BLMS.v2.Context;
using BLMS.v2.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.v2.Controllers
{
    public class AuditLogController : Controller
    {
        readonly AuditLogDbContext dbContext = new AuditLogDbContext();

        public IActionResult Index()
        {
            List<AuditLog> AuditLogList = dbContext.GetAuditLog().ToList();

            return View(AuditLogList);
        }
    }
}
