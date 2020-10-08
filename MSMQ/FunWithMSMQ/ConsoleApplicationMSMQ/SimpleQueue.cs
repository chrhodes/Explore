using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace ConsoleApplicationMSMQ
{
    class SimpleQueue : IDisposable
    {
        // Create some message queues that will stick around.

        MessageQueue _queueOrdersA1;
        MessageQueue _queueOrdersA2;
        MessageQueue _queueOrdersA3;
        MessageQueue _queueOrdersA4;

        private Boolean _PauseProcessing = false;
        public Boolean PauseProcessing
        {
            get
            {
                return _PauseProcessing;
            }
            set
            {
                _PauseProcessing = value;
            }
        }


		//**************************************************
		// Sends an Order to a queue.
        //**************************************************

        /// <summary>
        /// Initializes a new instance of the SimpleQueue class.
        /// </summary>
        public SimpleQueue()
        {
            _queueOrdersA1 = MSMQUtil.GetMessageQueue(".\\OrdersQueueA1", "OrdersQueueA1");
            _queueOrdersA2 = MSMQUtil.GetMessageQueue(".\\OrdersQueueA2", "OrdersQueueA2");
            _queueOrdersA3 = MSMQUtil.GetMessageQueue(".\\OrdersQueueA3", "OrdersQueueA3");
            _queueOrdersA4 = MSMQUtil.GetMessageQueue(".\\OrdersQueueA4", "OrdersQueueA4");
        }

        public void DeleteQueues()
        {
            MessageQueue.Delete(".\\OrdersQueueA1");
            MessageQueue.Delete(".\\OrdersQueueA2");
            MessageQueue.Delete(".\\OrdersQueueA3");
            MessageQueue.Delete(".\\OrdersQueueA4");
        }

        public void PurgeQueues()
        {
            _queueOrdersA1.Purge();
            _queueOrdersA2.Purge();
            _queueOrdersA3.Purge();
            _queueOrdersA4.Purge();
        }

        public void SendMessage(Order order, string queuePath, string queueName, Boolean makeRecoverable)
		{
            ProcessThreadUtil.DisplayInfo(string.Format("SM({0})-{1}", queueName, order));	

			// Connect to a queue on the local computer.

            using(MessageQueue myQueue = MSMQUtil.GetMessageQueue(queuePath, queueName))
            {
                try
                {
                    if (makeRecoverable)
                    {
                        Message message = new Message(order);
                        message.Recoverable = makeRecoverable;
                        myQueue.Send(message);
                    }
                    else
                    {
                        myQueue.Send(order);                       
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
		}

		//**************************************************
		// Receives a message containing an Order.
		//**************************************************
		
		public void ReceiveMessage(string queuePath, string queueName, int timeoutSeconds)
		{
            using (MessageQueue mq = MSMQUtil.GetMessageQueue(queuePath, queueName))
            {
                // Set the formatter to indicate body contains an Order.
                mq.Formatter = new XmlMessageFormatter(new Type[]{typeof(Order)});

                try
                {
                    Message myMessage;

                    if (timeoutSeconds != 0)
                    {
                        ProcessThreadUtil.DisplayInfo("RMTO(1)");
                		myMessage = mq.Receive(new TimeSpan(0,0,timeoutSeconds));
                        ProcessThreadUtil.DisplayInfo("RMTO(2)");

                        if(myMessage == null) return;
                    }
                    else
                    {
                        // Wait forever
                		 myMessage = mq.Receive();     
                    }

                	Order order = (Order)myMessage.Body;
                    ProcessThreadUtil.DisplayInfo(string.Format("RM({0}) {1}", queueName, order));
                }
                catch (MessageQueueException ex)
                {
                    // Handle Message Queuing exceptions.
                    ProcessThreadUtil.DisplayInfo(ex.Message);
                }			            
			    catch (InvalidOperationException ex)
			    {
                    // Handle invalid serialization format.
				    ProcessThreadUtil.DisplayInfo(ex.Message);
			    }
                catch (Exception ex)
                {
 			        // Catch other exceptions as necessary.    
                    ProcessThreadUtil.DisplayInfo(ex.Message);               
                }
            }
		}

        public void EnableReceiveMessageAsync()
        {
            ProcessThreadUtil.DisplayInfo("EnableReceiveMessageAsync()");
            try
            {
                _queueOrdersA1.ReceiveCompleted += GenericAsyncListener1;
                _queueOrdersA2.ReceiveCompleted += GenericAsyncListener2;
                _queueOrdersA3.ReceiveCompleted += GenericAsyncListener3;
                _queueOrdersA4.ReceiveCompleted += GenericAsyncListener4;

                _queueOrdersA1.BeginReceive();
                _queueOrdersA2.BeginReceive();
                _queueOrdersA3.BeginReceive();
                _queueOrdersA4.BeginReceive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            ProcessThreadUtil.DisplayInfo("EnableReceiveMessageAsync() Exit");
        }

        public void DisableReceiveMessageAsync1()
        {
            ProcessThreadUtil.DisplayInfo("DisableReceiveMessageAsync1()");

            // This approach loses the first message sent to the queue
            // after the disable.
            try
            {
                _queueOrdersA1.ReceiveCompleted -= GenericAsyncListener1;
                _queueOrdersA2.ReceiveCompleted -= GenericAsyncListener2;
                _queueOrdersA3.ReceiveCompleted -= GenericAsyncListener3;
                _queueOrdersA4.ReceiveCompleted -= GenericAsyncListener4;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DisableReceiveMessageAsync2()
        {
            ProcessThreadUtil.DisplayInfo("DisableReceiveMessageAsync2()"); 

            // This approach seems to leave the queues in a good state.

            try
            {
                _queueOrdersA1.ReceiveCompleted -= GenericAsyncListener1;
                _queueOrdersA2.ReceiveCompleted -= GenericAsyncListener2;
                _queueOrdersA3.ReceiveCompleted -= GenericAsyncListener3;
                _queueOrdersA4.ReceiveCompleted -= GenericAsyncListener4;
                _queueOrdersA1.Close();
                _queueOrdersA2.Close();
                _queueOrdersA3.Close();
                _queueOrdersA4.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void DisableReceiveMessageAsync3()
        {
            ProcessThreadUtil.DisplayInfo("DisableReceiveMessageAsync3()"); 
            try
            {
                _queueOrdersA1.ReceiveCompleted -= GenericAsyncListener1;
                _queueOrdersA2.ReceiveCompleted -= GenericAsyncListener2;
                _queueOrdersA3.ReceiveCompleted -= GenericAsyncListener3;
                _queueOrdersA4.ReceiveCompleted -= GenericAsyncListener4;

                try
                {
                    _queueOrdersA1.EndReceive(null);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                try
                {
                    _queueOrdersA2.EndReceive(null);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                try
                {
                    _queueOrdersA3.EndReceive(null);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                try
                {
                    _queueOrdersA4.EndReceive(null);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                //_queueOrdersA1.ReceiveCompleted -= GenericAsyncListener1;
                //_queueOrdersA2.ReceiveCompleted -= GenericAsyncListener2;
                //_queueOrdersA3.ReceiveCompleted -= GenericAsyncListener3;
                //_queueOrdersA4.ReceiveCompleted -= GenericAsyncListener4;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void GenericAsyncListener1(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = null;

            try
            {
                mq = (MessageQueue)source;
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                Message message = mq.EndReceive(asyncResult.AsyncResult);
                Order order = (Order)message.Body;

                ProcessThreadUtil.DisplayInfo(string.Format("GAL1 {0}", order));

                //mq.BeginReceive();
            }
            catch(MessageQueueException ex)
            {
                // Handle Message Queuing exceptions.
                Console.WriteLine(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                // Handle invalid serialization format.
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                // Catch other exceptions as necessary.    
                Console.WriteLine(ex.Message);               
            }
            finally
            {
                //mq.BeginReceive();
            }
        }

        private void GenericAsyncListener2(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = null;

            try
            {
                mq = (MessageQueue)source;
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                Message message = mq.EndReceive(asyncResult.AsyncResult);

                // NB.  It does not do any good to set the recoverable flag
                // after the message has been received.
                //message.Recoverable = true;

                Order order = (Order)message.Body;

                ProcessThreadUtil.DisplayInfo(string.Format("GAL2 {0}", order));

                if (PauseProcessing)
                {
                    ProcessThreadUtil.DisplayInfo("GAL2 - Paused");            	
                    return;
                }
                else
                {
                     mq.BeginReceive();               
                }
            }
            catch(MessageQueueException ex)
            {
                // Handle Message Queuing exceptions.
                Console.WriteLine(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                // Handle invalid serialization format.
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                // Catch other exceptions as necessary. 
                Console.WriteLine(ex.Message);                  
            }
            finally
            {
                //mq.BeginReceive();
            }
        }

        private void GenericAsyncListener3(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = null;

            try
            {
                mq = (MessageQueue)source;
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                Message message = mq.EndReceive(asyncResult.AsyncResult);
                Order order = (Order)message.Body;

                ProcessThreadUtil.DisplayInfo(string.Format("GAL3 {0}", order));

                mq.BeginReceive();
            }
            catch(MessageQueueException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch(InvalidOperationException ex)
            {
                // Handle invalid serialization format.
                Console.WriteLine(ex.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());                  
            }
            finally
            {
                mq.BeginReceive();
            }
        }

        private void GenericAsyncListener4(object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = null;

            if (PauseProcessing)
            {
                ProcessThreadUtil.DisplayInfo("GAL4 - Paused");            	
                return;
            }

            try
            {
                mq = (MessageQueue)source;
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Order) });
                ////Message message = mq.EndReceive(asyncResult.AsyncResult);
                //Order order = (Order)message.Body;
                //ProcessThreadUtil.DisplayInfo(string.Format("GAL4 {0}", order));
                //ProcessThreadUtil.DisplayInfo(string.Format("GAL4 {0}", order));

                //mq.BeginReceive();
            }
            catch(MessageQueueException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch(InvalidOperationException ex)
            {
                // Handle invalid serialization format.
                Console.WriteLine(ex.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());                 
            }
            finally
            {
                mq.BeginReceive();
            }
        }

        public void Dispose()
        {
            _queueOrdersA1.Dispose();
            _queueOrdersA2.Dispose();
            _queueOrdersA3.Dispose();
            _queueOrdersA4.Dispose();
        }

    }
}
