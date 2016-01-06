using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Andacol.Core.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Identifier { get; set; }

        public virtual List<AskedQuestion> AnsweredQuestions { get; set; }

    }
}