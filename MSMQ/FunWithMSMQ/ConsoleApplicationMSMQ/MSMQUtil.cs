using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

//using PacificLife.Life;

namespace ConsoleApplicationMSMQ
{
    class MSMQUtil
    {
        public static int CLASS_BASE_ERRORNUMBER = 0;
        private const string PLLOG_APPNAME = "FunWithMSMQ";

        public static Boolean Verbose { get; set; }
        public static Boolean Recoverable { get; set; }
        public static Boolean Journal { get; set; }

        public static MessageQueue GetMessageQueue(string queuePath, string queueName)
        {
            MessageQueue  messageQueue = new MessageQueue();

            if(MessageQueue.Exists(queuePath))
            {
                messageQueue.Path = queuePath;
                if (Verbose)
                {
                    ProcessThreadUtil.DisplayInfo(string.Format("GMQ({0}:{1}(exists)", queueName, messageQueue.GetHashCode()));                	;
                }
            }
            else
            {
                ProcessThreadUtil.DisplayInfo(string.Format("GMQ({0})(does not exist) ", queueName));
                try
                {
                    MessageQueue.Create(queuePath);
                    messageQueue.QueueName = queueName;

                    messageQueue.SetPermissions("System", MessageQueueAccessRights.FullControl);
                    messageQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);

                    messageQueue.DefaultPropertiesToSend.Recoverable = Recoverable;
                    messageQueue.UseJournalQueue = Journal;
                }
                catch(MessageQueueException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return messageQueue;
        }

        public static int GetMessageCount(MessageQueue q)
        {
            int count = 0;
            Cursor cursor = q.CreateCursor();

            Message m = PeekWithoutTimeout(q, cursor, PeekAction.Current);

            if (m != null)
            {
                count = 1;

                while ((m = PeekWithoutTimeout(q, cursor, PeekAction.Next)) != null)
                {
                    count++;
                }
            }

            return count;
        }

        protected static Message PeekWithoutTimeout(MessageQueue q, Cursor cursor, PeekAction action)
        {
            Message ret = null;

            try
            {
                ret = q.Peek(new TimeSpan(1), cursor, action);
            }
            catch (MessageQueueException mqe)
            {
                if (!mqe.Message.ToLower().Contains("timeout"))
                {
                    throw;
                }
            }

            return ret;
        }
    }
}
