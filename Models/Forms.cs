using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Models
{
    public class Forms
    {
        [Key]
        public Int64 FormsID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public string Phone { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string LoanAmount { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string DOB { get; set; }
        public string LicenceNo { get; set; }
        public string LicenceState { get; set; }
        public string IP { get; set; }
        public bool FormIsSubmit { get; set; }
        public string UserCreatedByUserID { get; set; }
        public DateTime UserCreatedDate { get; set; }
        public string UserUpdatedByUserID { get; set; }
        public DateTime? UserUpdatedDate { get; set; }
    }
}
