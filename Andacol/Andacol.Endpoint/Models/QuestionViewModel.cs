using Andacol.Core.Models;
using IKLSoftware.ODataHelpers;
using IKLSoftware.ODataHelpers.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.OData.Builder;

namespace Andacol.Endpoint.Models
{
    public class QuestionViewModel : ODataConfigurableEntity<QuestionViewModel>
    {

        [Key]
        public long Id { get; set; }

        public DateTime DateAsked { get; set; }

        public string QuestionText { get; set; }

        public QuestionType QuestionType { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public List<AnswerOption> Options { get; set; }

        public static void Configure(EntityTypeConfiguration<QuestionViewModel> config, ODataModelBuilder builder)
        {
            builder.EntitySet<AnswerOption>("Options");
        }

    }
}