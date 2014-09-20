using System;
using BitwiseCommerce.Orders.Contracts;
using BitwiseCommerce.Orders.InternalMessages;
using NServiceBus;

namespace BitwiseCommerce.Orders.Handlers
{
    public class OrderHandler
        : IHandleMessages<ProcessOrder>
    {
        public IBus Bus { get; set; }

        public void Handle(ProcessOrder message)
        {
            //persist order...
            Bus.Publish<IOrderAccepted>(x => x.Id = message.Id);
            Console.WriteLine("Order {0} accepted.", message.Id);
        }
    }
}
