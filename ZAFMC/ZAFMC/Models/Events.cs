using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZAFMC.Models
{
    [Table("Events")]    //SQL DBase Column--using TABLE data annotation
    public class Events
    {
        public string EVENTID { get; set; } //GUID

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string EventName { get; set; }

        [Range(typeof(DateTime), "01/01/2020", "01/01/2021", ErrorMessage = "Event date can't be less than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime EventDate { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string EventVenue { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string EventPerson { get; set; }

        [StringLength(15, ErrorMessage = "Digits Required(Min=9 & Max=15", MinimumLength = 9)]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$")]
        [Required]
        public string EventPersonCell { get; set; }

        [StringLength(40, ErrorMessage = "Characters Required(Min=2 & Max=40", MinimumLength = 2)]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$")]
        [Required]
        public string EventPersonEmail { get; set; }

        [StringLength(2, ErrorMessage = "Characters Required(Min=1 & Max=2", MinimumLength = 1)]
        [Range(1, 14, ErrorMessage = "Duration(Min=1 & Max=14)")]
        [Required]
        public string EventDuration { get; set; }

        [StringLength(12, ErrorMessage = "Characters Required(Min=8 & Max=12", MinimumLength = 8)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string EventStatus { get; set; }

    }
}