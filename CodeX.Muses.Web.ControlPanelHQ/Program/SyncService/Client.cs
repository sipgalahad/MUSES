using System.Collections.Generic;
using System.Threading;

namespace CodeX.Muses.Web.ControlPanel.Program
{
    public class Client
    {
        private ManualResetEvent messageEvent = new ManualResetEvent(false);
        private Queue<string> messageQueue = new Queue<string>();

        public void EnqueueMessage(string syncType)
        {
            lock (messageQueue)
            {
                messageQueue.Enqueue(syncType);
                messageEvent.Set();
            }
        }

        public string DequeueMessage()
        {
            messageEvent.WaitOne(new System.TimeSpan(0, 1, 0));

            lock (messageQueue)
            {
                if (messageQueue.Count < 1)
                    return "";
                if (messageQueue.Count == 1)
                {
                    messageEvent.Reset();
                }
                return messageQueue.Dequeue();
            }
        }
    }
}