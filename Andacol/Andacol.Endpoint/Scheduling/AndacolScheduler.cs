using Andacol.Core.Models;
using Andacol.Core.Extensions;
using Andacol.Endpoint.Properties;
using Andacol.Endpoint.Scheduling;
using System;
using System.Threading;
using System.Web;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

[assembly: PreApplicationStartMethod(typeof(AndacolScheduler), "Start")]
public static class AndacolScheduler
{
    private static Timer Timer { get; } = new Timer(OnTimerElapsed);

    private static JobHost JobHost { get; } = new JobHost();

    public static int TimesScheduled { get; private set; } = 0;

    public static void Start() => Timer.Change(TimeSpan.Zero, Settings.Default.QuestionRefreshInterval);

    private static void OnTimerElapsed(object sender)
    {
        JobHost.DoWork(() => {
            using (var db = new AndacolContext())
            {
                try
                {
                    db.AskQuestions();
                    TimesScheduled++;
                }
                catch (Exception ex) {
                    Debug.Print(ex.Message);
                }
            }
        });
    }

    public static void AskQuestions(this AndacolContext db)
    {
        db.Questions.WhereDue().ToList().ForEach(q =>
        {
            while (q.NextDue <= DateTime.UtcNow)
            {
                db.AskedQuestions.Add(new AskedQuestion
                {
                    DateAsked = q.NextDue,
                    Question = q
                });
                q.NextDue += q.Schedule;
            }
        });
        db.SaveChanges();
    }
}