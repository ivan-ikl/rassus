using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Andacol.Core.Models
{
    public class AnswerOption
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public virtual OptionalQuestion OptionalQuestion { get; set; }

        public virtual List<OptionAnswer> OptionAnswers { get; set; }

    }
}