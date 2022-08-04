using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZAFMC.Models
{
    [Table("Churches")]    //SQL DBase Column--using TABLE data annotation
    public class Churches
    {
         public string CHURCHID { get; set; }    //GUID

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchName { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchLeader { get; set; }


        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string ChrchLeaderCell { get; set; }
        
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$")]
        [Required]
        public string ChrchLeaderEmail { get; set; }

        [Range(typeof(DateTime), "01/01/1920", "01/01/2020", ErrorMessage = "Opened Date can't be greater than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime ChrchDateOpened { get; set; }

        [RegularExpression(@"^[1-9]{1}$|^[1-4]{1}[0-9]{1}$|10500$")]   //Maximum number 10500
        [Required]
        public long ChrchNumMembers { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchProvince { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchDistrict { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchZone { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchSection { get; set; }

        [StringLength(500, ErrorMessage = "Characters Required(Min=2 & Max=500", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchPastor { get; set; }

        [StringLength(500, ErrorMessage = "Characters Required(Min=2 & Max=500", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchEvangelist { get; set; }

        [StringLength(500, ErrorMessage = "Characters Required(Min=2 & Max=500", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchPreachers { get; set; }

        [StringLength(500, ErrorMessage = "Characters Required(Min=2 & Max=500", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchProphets { get; set; }

        [StringLength(500, ErrorMessage = "Characters Required(Min=2 & Max=500", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchDecons { get; set; }

        [StringLength(10500, ErrorMessage = "Characters Required(Min=2 & Max=10500", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ChrchMembers { get; set; }

    }
}