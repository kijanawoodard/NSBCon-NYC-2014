using System;
using System.Net;
using BitwiseCommerce.Orders.InternalMessages;
using NServiceBus;

namespace BitwiseCommerce.Api
{
    public class OrderController
    {
        public IBus Bus { get; set; }

        public HttpStatusCode Post(ProcessOrder order)
        {
            Bus.Send(order);
            Console.WriteLine("Order {0} sent.", order.Id);
            return HttpStatusCode.OK;
        }
    }
}
