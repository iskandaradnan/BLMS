using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BLMS.Models.Admin
{
    public class BusinessDiv
    {
        [Key]
        public int DivID { get; set; }

        public string OldDivName { get; set; }

        [Column(TypeName = "nvarchar(350)")]
        [DisplayName("Business Division")]
        public string DivName { get; set; }

        //check duplicate
        public int ExistData { get; set; }

        //check linked data
        public int LinkedData { get; set; }

        public string UserName { get; set; }
    }
}
