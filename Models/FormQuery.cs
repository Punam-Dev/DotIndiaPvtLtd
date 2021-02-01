using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Models
{
    public class FormQuery
    {
        [Key]
        public Int64 FormQueryID { get; set; }
        public string FormQueryText { get; set; }
        public string FormQueryStatus { get; set; }
        public string FormQueryCreatedByUserID { get; set; }
        public DateTime FormQueryCreatedDate { get; set; }
        public string FormQueryUpdatedByUserID { get; set; }
        public DateTime? FormQueryUpdatedDate { get; set; }
    }
}
