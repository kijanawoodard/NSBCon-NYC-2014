using System;
using BitwiseCommerce.Billing.Contracts;
using BitwiseCommerce.Orders.Contracts;
using NServiceBus;

namespace BitwiseCommerce.Billing.Handlers
{
    public class BillingHandler
        : IHandleMessages<IOrderAccepted>
    {
        public IBus Bus { get; set; }

        public void Handle(IOrderAccepted message)
        {
            Bus.Publish<IOrderBilled>(x => x.Id = message.Id);    
            Console.WriteLine("Order {0} billed.", message.Id);
        }
    }
}