using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace ConsoleApplicationMSMQ
{
    class Program
    {
        static SimpleQueue _simpleQueue;
        static Boolean _isRecoverable = false;
        static Boolean _isJournal = false;
        static Boolean _isInitialized = false;

        private static void DisplayHelp()
        {
            Console.WriteLine("EA:   EnableAsyncReceive");
            Console.WriteLine("DA1:  DisableAsyncReceive1");
            Console.WriteLine("DA2:  DisableAsyncReceive2");
            Console.WriteLine("DA3:  DisableAsyncReceive3");
            Console.WriteLine("PQ:   PurgeAllQueues");
            Console.WriteLine("SQ:   SendQueue");
            Console.WriteLine("SQR:  SendQueueRecoverable");
            Console.WriteLine("SQ1:  SendAsyncQueue1");
            Console.WriteLine("SQ2:  SendAsyncQueue2");
            Console.WriteLine("SQ3:  SendAsyncQueue3");
            Console.WriteLine("SQ4:  SendAsyncQueue4");
            Console.WriteLine("SCQ:  SendCHRQueue");
            Console.WriteLine("RQ:   ReadQueue");
            Console.WriteLine("RQR:  ReadQueueRecoverable");
            Console.WriteLine("RQTO: ReadQueueTimeout");
            Console.WriteLine("RQ1:  ReadAsyncQueue1");
            Console.WriteLine("R:    Recoverable");
            Console.WriteLine("J:    Journal Queues");
            Console.WriteLine("V:    Verbose GetQueue");
            Console.WriteLine("P:    Pause processing");
            Console.WriteLine("X:    Exit");
            Console.WriteLine("?:    Help");
        }

        private static void Initialize()
        {
            _simpleQueue = new SimpleQueue();
            _isInitialized = true;
        }

        static void Main(string[] args)
        {
            DisplayHelp();

            ProcessThreadUtil.DisplayInfo("Main(1)");

            Boolean keepLooping = true;

            while(keepLooping)
            {
                DisplayPrompt();

                switch(Console.ReadLine().ToUpper())
                {
                    case "I":
                        Initialize();
                        break;

                    case "CQ":
                        CreateQueues();
                        break;

                    case "DQ":
                        DeleteQueues();
                        break;

                    case "EA":
                        EnableAsyncReceive();
                        break;

                    case "DA1":
                        DisableAsyncReceive1();
                        break;

                    case "DA2":
                        DisableAsyncReceive2();
                        break;

                    case "DA3":
                        DisableAsyncReceive3();
                        break;

                    case "PQ":
                        PurgeAllQueues();
                        break;

                    case "SQ":
                        SendQueue();
                        break;

                    case "SQ1":
                        SendAsyncQueue1();
                        break;

                    case "SQ2":
                        SendAsyncQueue2();
                        break;

                    case "SQ3":
                        SendAsyncQueue3();
                        break;

                    case "SQ4":
                        SendAsyncQueue4();
                        break;

                    case "SCQ":
                        SendCHRQueue();
                        break;

                    case "RQ":
                        ReadQueue();
                        break;

                    case "RQTO":
                        ReceiveQueueTimeout();
                        break;

                    case "RAQ1":
                        ReadAsyncQueue1();
                        break;

                    case "RCQ":
                        ReceiveCHRQueue();
                        break;

                    case "R":
                        _isRecoverable = ! _isRecoverable;
                        MSMQUtil.Recoverable = _isRecoverable;
                        Console.WriteLine(string.Format("Recoverable:{0}",_isRecoverable));
                        break;
                    
                    case "J":
                        _isJournal = !_isJournal;
                        MSMQUtil.Journal = _isJournal;
                        Console.WriteLine(string.Format("Journal:{0}", _isJournal));
                        break;

                    case "V":
                        MSMQUtil.Verbose = ! MSMQUtil.Verbose;
                        break;

                    case "P":
                        PauseProcessing();
                        break;

                    case "X":
                        Console.WriteLine("Exiting");
                        keepLooping = false;
                        break;

                    case "?":
                        DisplayHelp();
                        break;

                    default:
                        Console.WriteLine("??");
                        break;
                }
            }
        }

        private static void DisplayPrompt()
        {
            if (_isInitialized)
            {
                int myQueueCount;
                int chrQueueCount;
                int ordersQueueA1Count;
                int ordersQueueA2Count;
                int ordersQueueA3Count;
                int ordersQueueA4Count;

                using (MessageQueue mq = MSMQUtil.GetMessageQueue(".\\myQueue", "MyQueue"))
                {
                    myQueueCount = MSMQUtil.GetMessageCount(mq);
                }

                using(MessageQueue mq = MSMQUtil.GetMessageQueue(".\\OrdersQueueA1", "OrdersQueueA1"))
                {
                    ordersQueueA1Count = MSMQUtil.GetMessageCount(mq);
                }

                using(MessageQueue mq = MSMQUtil.GetMessageQueue(".\\OrdersQueueA2", "OrdersQueueA2"))
                {
                    ordersQueueA2Count = MSMQUtil.GetMessageCount(mq);
                }

                using(MessageQueue mq = MSMQUtil.GetMessageQueue(".\\OrdersQueueA3", "OrdersQueueA3"))
                {
                    ordersQueueA3Count = MSMQUtil.GetMessageCount(mq);
                }

                using(MessageQueue mq = MSMQUtil.GetMessageQueue(".\\OrdersQueueA4", "OrdersQueueA4"))
                {
                    ordersQueueA4Count = MSMQUtil.GetMessageCount(mq);
                }

                using(MessageQueue mq = MSMQUtil.GetMessageQueue(".\\private$\\CHRQueue", "CHRQueue"))
                {
                    chrQueueCount = MSMQUtil.GetMessageCount(mq);
                }

                Console.Write(string.Format("MQ({0}) OQA1({1}) OQA2({2}) OQA3({3}) OQA4({4}) CHRQ({5}) > ", 
                    myQueueCount, ordersQueueA1Count, ordersQueueA2Count, ordersQueueA3Count, ordersQueueA4Count, chrQueueCount));               
            }
            else
            {
                Console.Write("Need to Init >");
            }

        }

        private static void CreateQueues()
        {
            throw new NotImplementedException();
        }

        private static void DeleteQueues()
        {
            SimpleQueue sq = new SimpleQueue();
            sq.DeleteQueues();
            _isInitialized = false;
        }

        private static void EnableAsyncReceive()
        {
            //SimpleQueue queue = new SimpleQueue();
            Console.WriteLine(_simpleQueue.GetHashCode().ToString());
            _simpleQueue.EnableReceiveMessageAsync();
        }

        private static void DisableAsyncReceive1()
        {
            Console.WriteLine(_simpleQueue.GetHashCode().ToString());
            _simpleQueue.DisableReceiveMessageAsync1();
        }


        private static void DisableAsyncReceive2()
        {
            Console.WriteLine(_simpleQueue.GetHashCode().ToString());
            // Don't really understand why this drops the first sent message after
            // re-enabling
            _simpleQueue.DisableReceiveMessageAsync2();
            //try
            //{
            //    _simpleQueue.Dispose();
            //    _simpleQueue = null;
            //    Initialize();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }

        private static void DisableAsyncReceive3()
        {
            Console.WriteLine(_simpleQueue.GetHashCode().ToString());
            _simpleQueue.DisableReceiveMessageAsync3();
        }

        private static void SendQueue()
        {
			SendQueue(".\\myQueue", "MyQueue", _isRecoverable, 1);
        }

        private static void SendCHRQueue()
        {
			SendQueue(".\\private$\\CHRQueue", "CHRQueue", _isRecoverable, 1);
        }

        private static void SendQueue(string queuePath, string queueName, Boolean recoverable, int count)
        {
            //SimpleQueue sq = new SimpleQueue();
            SimpleQueue sq = _simpleQueue;

            for(int i = 0 ; i < count; i++)
            {
                sq.SendMessage(new Order(i), queuePath, queueName, recoverable);
            }
        }

        private static void SendAsyncQueue1()
        {
			SendQueue(".\\OrdersQueueA1", "OrdersQueueA1", _isRecoverable, 10);
        }

        private static void SendAsyncQueue2()
        {
			SendQueue(".\\OrdersQueueA2", "OrdersQueueA2", _isRecoverable, 10);
        }

        private static void SendAsyncQueue3()
        {
			SendQueue(".\\OrdersQueueA3", "OrdersQueueA3", _isRecoverable, 10);
        }

        private static void SendAsyncQueue4()
        {
			SendQueue(".\\OrdersQueueA4", "OrdersQueueA4", _isRecoverable, 10);
        }

        private static void ReadQueue()
        {
			SimpleQueue myNewQueue = new SimpleQueue();

			myNewQueue.ReceiveMessage(".\\myQueue", "MyQueue", 0);
        }

        private static void ReceiveQueueTimeout()
        {
			SimpleQueue sq = new SimpleQueue();

			sq.ReceiveMessage(".\\myQueue", "MyQueue", 5);
        }

        private static void ReadAsyncQueue1()
        {
			SimpleQueue myNewQueue = new SimpleQueue();

			myNewQueue.ReceiveMessage(".\\OrdersQueueA1", "OrdersQueueA1", 0);
        }

        private static void ReceiveCHRQueue()
        {
			SimpleQueue myNewQueue = new SimpleQueue();

			myNewQueue.ReceiveMessage(".\\private$\\CHRQueue", "CHRQueue", 0);
        }

        private static void PurgeAllQueues()
        {
			SimpleQueue sq = new SimpleQueue();
            sq.PurgeQueues();
        }

        private static void PauseProcessing()
        {
            _simpleQueue.PauseProcessing = ! _simpleQueue.PauseProcessing;
            Console.WriteLine("Paused: " + _simpleQueue.PauseProcessing);
        }
    }
}
