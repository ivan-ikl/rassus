using System.ComponentModel.DataAnnotations;

namespace Andacol.Endpoint.Models
{
    public class Option
    {
        [Key]
        public long Id { get; set; }

        public string Text { get; set; }

        public long Votes { get; set; }

    }
}