using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Dtos
{
    public class VerifyOTPDto
    {
        [Required]
        public int OTP { get; set; }
    }
}
