using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andacol.Core.Models
{
    public class AskedQuestion
    {
        [Key]
        public long Id { get; set; }

        [Index]
        public DateTime DateAsked { get; set; } = DateTime.UtcNow;

        [Required]
        public virtual Question Question { get; set; }

        public virtual List<Answer> Answers { get; set; }

        public virtual List<User> AnsweredBy { get; set; }

    }
}