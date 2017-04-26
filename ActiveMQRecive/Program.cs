using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveMQRecive
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region 消息消费者
                string queuesName = "myQueue";
                Uri _uri = new Uri(String.Concat("activemq:tcp://localhost:61616?wireFormat.maxInactivityDuration=0"));
                IConnectionFactory factory = new ConnectionFactory(_uri);
                // IConnectionFactory factory = new ConnectionFactory();
                using (IConnection conn = factory.CreateConnection("admin", "manager"))
                {
                    using (ISession session = conn.CreateSession())
                    {
                        conn.Start();
                        IDestination destination = SessionUtil.GetDestination(session, queuesName);
                        using (IMessageConsumer consumer = session.CreateConsumer(destination))
                        {
                            consumer.Listener += (IMessage message) =>
                            {
                                ITextMessage msg = (ITextMessage)message;
                                Console.WriteLine("接收消息：" + msg.Text);
                            };
                            // consumer.Listener += new MessageListener(consumer_Listener);
                            Console.ReadLine();
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void consumer_Listener(IMessage message)
        {
            try
            {
                ITextMessage msg = (ITextMessage)message;
                Console.WriteLine("接收：" + msg.Text);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
