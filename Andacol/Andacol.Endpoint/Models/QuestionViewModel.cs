using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andacol.Endpoint.Models
{
    public class QuestionViewModel
    {

        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Kraj upitnika")]
        public DateTime DateExpired { get; set; } = DateTime.MaxValue;

        [Required]
        [Display(Name = "Početak upitnika")]
        public DateTime DateStarted { get; set; } = DateTime.MaxValue;

        [NotMapped]
        public long ScheduleTicks { get; set; }

        [Required]
        [Display(Name = "Interval upitnika")]
        public TimeSpan Schedule {
            get
            {
                return TimeSpan.FromTicks(ScheduleTicks);
            }
            set {
                ScheduleTicks = value.Ticks;
            }
        }

        [Required]
        [Display(Name = "Pitanje")]
        public string QuestionText { get; set; }

        public IEnumerable<Option> Options { get; set; }

        [Display(Name = "Najmanja vrijednost")]
        public int Min { get; set; } 

        [Display(Name = "Najveća vrijednost")]
        public int Max { get; set; }

        public IEnumerable<Score> Scores { get; set; }

        [Display(Name = "Vrsta upitnika")]
        public QuestionType QuestionType { get; set; }

    }
}