using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZAFMC.Models
{
    [Table("Media")]    //SQL DBase Column--using TABLE data annotation
    public class Media
    {
        public string MEDIAID { get; set; }  //GUID

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string NewsName { get; set; }

        [Range(typeof(DateTime), "01/01/2010", "01/04/2020", ErrorMessage = "News date can't be greather than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime NewsDate { get; set; }

        [Required]
        public string NewsFile { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string SermonPreacher { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string SermonEventName { get; set; }

        [Range(typeof(DateTime), "01/01/2010", "01/04/2020", ErrorMessage = "Sermon date can't be greather than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime SermonDate { get; set; }

        [StringLength(20, ErrorMessage = "Characters Required(Min=3 & Max=20", MinimumLength = 3)]
        [Required]
        [RegularExpression(@"^(([1-9]+[A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+[1-9]))$")]
        public string SermonVerse { get; set; }

        [Required]
        public string SermonFile { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string VideoName { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string VideoOccassion { get; set; }

        [Range(typeof(DateTime), "01/01/2010", "01/04/2020", ErrorMessage = "Video date can't be greather than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime VideoDate { get; set; }
        [Required]
        public string VideoFile { get; set; }

        [StringLength(15, ErrorMessage = "Characters Required(Min=2 & Max=15", MinimumLength = 2)]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+))$")]
        public string TestmTestifier { get; set; }

        [Range(typeof(DateTime), "01/01/2010", "01/04/2020", ErrorMessage = "Testimony date can't be greather than today")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        public DateTime TestmDate { get; set; }
        
        [Required]
        public string TestmFile { get; set; }

    }
}