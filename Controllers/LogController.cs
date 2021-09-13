using BLMS.v2.Context;
using BLMS.v2.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.v2.Controllers
{
    public class LogController : Controller
    {
        readonly AuditLogDbContext dbContext = new AuditLogDbContext();

        public IActionResult Index()
        {
            List<Log> AuditLogList = dbContext.GetAuditLog().ToList();

            return View(AuditLogList);
        }
    }
}
