using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.v2.Models.Admin
{
    public class AuditLog
    {
        public int ID { get; set; }

        public string Command { get; set; }

        public string SPName { get; set; }

        public string ScreenPath { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDt { get; set; }
    }
}
