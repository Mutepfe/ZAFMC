using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZAFMC.Models
{
    [Table("Projects")]    //SQL DBase Column--using TABLE data annotation
    public class Projects
    {
        public string PROJECTID { get; set; }  //GUID
        public string ProjectName { get; set; }

        [Range(typeof(DateTime), "01/01/2020", "01/01/2030", ErrorMessage = "Start date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime ProjectStartDate { get; set; }

        [StringLength(4, ErrorMessage = "Digits Required(Min=1 & Max=4", MinimumLength = 1)]
        [Required]
        [Range(1, 3650, ErrorMessage = "Duration(Min=1 Day & Max=10 Years)")]
        public string ProjectDuration { get; set; }

        [Range(typeof(DateTime), "01/01/2020", "01/01/2030", ErrorMessage = "Expected date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime ProjectExpecDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ProjectLeader { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ProjectLeaderRank { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string ProjectSite { get; set; }


        [StringLength(28, ErrorMessage = "Characters Required(Min=2 & Max=28", MinimumLength = 2)]
        [RegularExpression(@"^$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$")]
        [Required]
        public ulong ProjectEstimCost { get; set; }

        [Range(typeof(DateTime), "01/04/2020", "01/01/2030", ErrorMessage = "Completion date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime ProjectCompDate { get; set; }
    }

}