using System.ComponentModel.DataAnnotations;

namespace Andacol.Core.Models
{
    public abstract class Answer
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public virtual AskedQuestion AskedQuestion { get; set; }
    }

    public class ScoreAnswer : Answer
    {
        public int Score { get; set; }
    }

    public class OptionAnswer : Answer
    {
        public virtual AnswerOption AnswerOption { get; set; }
    }
}