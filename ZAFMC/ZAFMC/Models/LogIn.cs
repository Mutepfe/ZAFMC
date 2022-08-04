using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZAFMC.Models
{
    [Table("LogIn")]    //SQL DBase Column--using TABLE data annotation
    [ValidateAntiForgeryToken]
    public class LogIn
    {
        public string LOGINID { get; set; } //GUID

        [StringLength(12, ErrorMessage = "Characters Required(Min=8 & Max=12", MinimumLength = 8)]
        [Required]
        public string LogInPassportID { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=6 & Max=20", MinimumLength = 6)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z).{6,20})")]  //Between 6 to 20 Characters
        [Required]
        public string LogInPassword { get; set; }


        [RegularExpression(@"^[1-9]{1}$|^[1-4]{1}[0-9]{1}$|4$")]
        [Range(4, 4, ErrorMessage = "OTP-Number(Min=4 & Max=4)")]
        [Required]
        public int LogInOTP { get; set; }
    }
}