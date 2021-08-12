using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Models.Admin
{
    public class BusinessUnit
    {
        [Key]
        public int UnitID { get; set; }

        public int DivID { get; set; }

        public string OldUnitName { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Business Division")]
        public string DivName { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [DisplayName("Business Unit")]
        public string UnitName { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [DisplayName("Head of Company")]
        public string HoCName { get; set; }

        //check duplicate
        public int ExistData { get; set; }

        public string UserName { get; set; }
    }
}
