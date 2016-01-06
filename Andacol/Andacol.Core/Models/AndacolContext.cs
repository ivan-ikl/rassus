using System.Data.Entity;

namespace Andacol.Core.Models
{
    public class AndacolContext : DbContext
    {
        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<AnswerOption> AnswerOptions { get; set; }

        public virtual DbSet<AskedQuestion> AskedQuestions { get; set; }

        public virtual DbSet<Answer> Answers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("andacol");
            base.OnModelCreating(modelBuilder);
        }

    }
}
