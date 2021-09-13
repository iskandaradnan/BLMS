using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Models.Admin
{
    public class ErrorLog
    {

        public int ID { get; set; }

        public string PageName { get; set; }

        public string ErrorMessage { get; set; }

        public string Method { get; set; }

        public string LineNumber { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDt { get; set; }

    }
}
