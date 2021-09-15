using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Models.Admin
{
    public class AuditErrorViewModel
    {
        public List<ErrorLog> errorLog { get; set; }
        public List<Log> auditLog { get; set; }
    }

}
