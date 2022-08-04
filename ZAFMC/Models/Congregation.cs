using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace ZAFMC.Models
{
    [Table("Congregation")]    //SQL DBase Column--using TABLE data annotation
    public class Congregation
    {
         public string CONGREGATIONID { get; set; } //GUID    

        [StringLength(4, ErrorMessage = "Characters Required(Min=2 & Max=4", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+[\.]))$")]
        public string CongTitle { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongName { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongSurname { get; set; }

        [Range(typeof(DateTime), "01/01/1920", "01/01/2020", ErrorMessage = "DOB can't be greater than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime CongDOB { get; set; }

        [StringLength(6, ErrorMessage = "Characters Required(Min=4 & Max=6", MinimumLength = 4)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongGender { get; set; }
        public string CongPassportID { get; set; }

        [StringLength(10, ErrorMessage = "Characters Required(Min=6 & Max=10", MinimumLength = 6)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongStatus { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=5 & Max=15", MinimumLength = 5)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongProfession { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongKin { get; set; }
        public string CongKinContact { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"/^(\+\d{1,3}[- ]?)?\d{10}$/")]
        [Required]
        public string CongMobile { get; set; }


        public string CongAddress { get; set; }


        [StringLength(40, ErrorMessage = "Characters Required(Min=5 & Max=40", MinimumLength = 5)]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$")]
        [Required]
        public string CongEmail { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongPosition { get; set; }

        [Range(typeof(DateTime), "01/01/1920", "01/01/2020", ErrorMessage = "Appointed Date can't be greater than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime CongDateAppointed { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=3 & Max=25", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongManagerialPost { get; set; }

        [Range(typeof(DateTime), "01/01/1920", "01/01/2020", ErrorMessage = "Elected Date can't be greater than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime CongDateElected { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongProvince { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongDistrict { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongZone { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=2 & Max=20", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongSection { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongSSeniorLeader { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string CongViceLeader { get; set; }

        [RegularExpression(@"[^\s]+(?=\.jpg|gif|png))\.\2")]
        [Required]
        public string CongPhoto { get; set; }
        public string CongIDPass { get; set; }
                                    

    }
}