using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Models.Admin
{
    public class PIC
    {
        [Key]
        public int PICID { get; set; }

        public string OldPICStaffNo { get; set; }

        public int OldUserTypeID { get; set; }

        [DisplayName("Staff No")]
        public string PICStaffNo { get; set; }

        [Column(TypeName = "nvarchar(350)")]
        [DisplayName("Name")]
        public string PICName { get; set; }

        [Column(TypeName = "nvarchar(350)")]
        [DisplayName("Email")]
        public string PICEmail { get; set; }

        public int UserTypeID { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("User Type")]
        public string UserType { get; set; }

        //check linked data
        public int ExistData { get; set; }

        public string UserName { get; set; }
    }
}
