using System;
using System.Web.Hosting;

namespace Andacol.Endpoint.Scheduling
{
    public class JobHost : IRegisteredObject
    {
        private object Lock { get; } = new object();

        private bool ShuttingDown { get; set; }

        public JobHost()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Stop(bool immediate)
        {
            lock (Lock)
            {
                ShuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

        public void DoWork(Action work)
        {
            lock (Lock)
            {
                if (!ShuttingDown)
                {
                   work();
                }
            }
        }
    }
}