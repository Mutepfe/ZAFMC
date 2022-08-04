using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZAFMC.Models
{
    [Table("Passovers")]    //SQL DBase Column--using TABLE data annotation
    public class Passovers
    {
        public string PASSOVERID { get; set; } //GUID

        [Range(typeof(DateTime), "01/01/2010", "01/04/2020", ErrorMessage = "Passover date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime PassoverDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string PassoverVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string PassoverVenueLeader { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string PassoverLeaderCell { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/04/2021", ErrorMessage = "Passover date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime MemDate { get; set; }
        
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string MemDeceasedName { get; set; }


        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string MemVenue { get; set; }


        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string MemChurchLeader { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string MemRelativeCell { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/04/2021", ErrorMessage = "Big Sunday date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime BigSunDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string BigSunVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string BigSunLeader { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string BigSunLeaderCell { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/04/2021", ErrorMessage = "Graduation date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime GradDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string GradVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string GradName { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string GradCell { get; set; }

        [StringLength(30, ErrorMessage = "Characters Required(Min=2 & Max=30", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string GradDegree { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string GradLeader { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string GradLeaderCell { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/04/2021", ErrorMessage = "Youth conference can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime YouthDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string YouthVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string YouthLeader { get; set; }
        
        [StringLength(1, ErrorMessage = "Characters Required(Min=1 & Max=1", MinimumLength = 1)]
        [Required]
        [Range(1,5,ErrorMessage ="Duration(Min=1 Day & Max=5 Days)")]
        public string YouthDuration { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/04/2021", ErrorMessage = "Women conference can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime WomenDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string WomenVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string WomenLeader { get; set; }


        [StringLength(1, ErrorMessage = "Characters Required(Min=1 & Max=1", MinimumLength = 1)]
        [Required]
        [Range(1, 7, ErrorMessage = "Duration(Min=1 Day & Max=7 Days)")]
        public string WomenDuration { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/04/2021", ErrorMessage = "General meeting can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime GenDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string GenDescription { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string GenVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string GenLeader { get; set; }


        [StringLength(1, ErrorMessage = "Characters Required(Min=1 & Max=1", MinimumLength = 1)]
        [Required]
        [Range(1, 14, ErrorMessage = "Duration(Min=1 Day & Max=14 Days)")]
        public string GenDuration { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/04/2021", ErrorMessage = "Wedding date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime WeddDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string WeddVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string WeddBride { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string WeddBrideCell { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string WeddGroom { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string WeddGroomCell { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string WeddOfficer { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string WeddLeader { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string WeddLeaderCell { get; set; }

    }
}