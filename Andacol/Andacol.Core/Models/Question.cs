using System;
using Andacol.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Andacol.Core.Models
{
    public abstract class Question : IExpireable
    {
        [Key]
        public long Id { get; set; }

        [Required, Index]
        public DateTime DateExpired { get; set; } = DateTime.MaxValue;

        [Required, Index]
        public DateTime DateStarted { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Next time when the question is to be asked, for caching purposes. 
        /// </summary>
        [Index]
        public DateTime NextDue { get; set; } = DateTime.UtcNow;

        [Required]
        public TimeSpan Schedule { get; set; } = TimeSpan.FromDays(1);

        public virtual List<AskedQuestion> AskedQuestions { get; set; }

        public string QuestionText { get; set; }

    }

    public class OptionalQuestion : Question
    {
        public virtual List<AnswerOption> AnswerOptions { get; set; }

    }

    public class ScoreQuestion : Question
    {
        public int Min { get; set; } = 1;

        public int Max { get; set; } = 5;
    }
}
