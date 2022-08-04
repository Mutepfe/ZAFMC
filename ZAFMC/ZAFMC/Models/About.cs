using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZAFMC.Models
{
    
    [Table("AboutLeadership")]       //SQL DBase Column--using TABLE data annotation
    public class AboutValidation
    {
        public string ABOUTID { get; set; } //GUID

        [StringLength(15,ErrorMessage ="Characters Required(Min=2 & Max=15",MinimumLength =2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string LeaderName { get; set; }
       
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string LeaderSurname { get; set; }
        
        [Range(typeof(DateTime), "01/01/1900","01/01/2020",ErrorMessage ="DOB can't be greater than today")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:d}")]
        [Required]
        public DateTime LeaderDOB { get; set; }
        
        [StringLength(15, ErrorMessage = "Characters Required(Min=7 & Max=15", MinimumLength = 7)]
        [Required]
        public string LeaderIdentity { get; set; }
        
        [StringLength(30, ErrorMessage = "Characters Required(Min=5 & Max=30", MinimumLength = 5)]
        [Required]
        public string LeaderPosition { get; set; }
        
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        public string AdminName { get; set; }
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string AdminSurname { get; set; }
       
        [Required]
        public DateTime AdminAppntDate { get; set; }
        
        [StringLength(30, ErrorMessage = "Characters Required(Min=5 & Max=30", MinimumLength = 5)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string AdminInterComm { get; set; }
        
        [StringLength(30, ErrorMessage = "Characters Required(Min=2 & Max=30", MinimumLength = 5)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string AdminInterDir { get; set; }
        
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string DecName { get; set; }
       
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string DecSurname { get; set; }
       
        [StringLength(15, ErrorMessage = "Characters Required(Min=7 & Max=15", MinimumLength = 2)]
        [Required]
        public string DecID { get; set; }
       
        [Range(typeof(DateTime), "01/01/1900", "01/01/2020", ErrorMessage = "Death Date can't be greater than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime DecDOD { get; set; }
        
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string DecPosition { get; set; }
        
        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string DecBurialPlace { get; set; }

        [Range(typeof(DateTime), "01/01/1920", "01/04/2020", ErrorMessage = "Opened Date can't be greater than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime DecMemorial { get; set; }

    }

}