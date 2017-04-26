using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActiveMQSend
{
    class Program
    {
        static void Main(string[] args)
        {
            string queuesName = "myQueue";
            Uri _uri = new Uri(String.Concat("activemq:tcp://localhost:61616"));
            IConnectionFactory factory = new ConnectionFactory(_uri);
            //IConnectionFactory factory = new ConnectionFactory();
            using (IConnection conn = factory.CreateConnection("admin", "manager"))
            {
                using (ISession session = conn.CreateSession())
                {
                    IDestination destination = SessionUtil.GetDestination(session, queuesName);
                    using (IMessageProducer producer = session.CreateProducer(destination))
                    {
                        conn.Start();
                        //可以写入字符串，也可以是一个xml字符串等
                        for (int i = 0; i < 100; i++)
                        {
                            ITextMessage request = session.CreateTextMessage("新新messsage" + i);
                            producer.Send(request);
                            Console.WriteLine("发送新新消息：" + i);
                            Thread.Sleep(1000);

                        }

                    }
                }
            }
        }
    }
}
