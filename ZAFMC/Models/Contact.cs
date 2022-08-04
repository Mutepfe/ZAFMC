using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZAFMC.Models
{

    [Table("Contact")]    //SQL DBase Column--using TABLE data annotation
    public class Contact
    {
        public string CONTACTID { get; set; }  //GUID

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ContName { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ContPosition { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"/^(\+\d{1,3}[- ]?)?\d{10}$/")]
        [Required]
        public string ContMobile { get; set; }

        [StringLength(40, ErrorMessage = "Characters Required(Min=2 & Max=40", MinimumLength = 2)]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$")]
        [Required]
        public string ContMail { get; set; }

        [StringLength(50, ErrorMessage = "Characters Required(Min=5 & Max=50", MinimumLength = 5)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ContAddress { get; set; }
    }
}