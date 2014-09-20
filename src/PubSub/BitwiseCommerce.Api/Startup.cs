
using System;
using BitwiseCommerce.Orders.Handlers;
using BitwiseCommerce.Orders.InternalMessages;
using NServiceBus;

namespace BitwiseCommerce.Api
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            var controller = new OrderController(new OrderHandler(){Bus = Bus});
            controller.Bus = Bus;
            var orderNumber = 1;

            Schedule
                .Every(TimeSpan.FromSeconds(2))
                .Action(() => controller.Post(new ProcessOrder(orderNumber++.ToString())));

            while (true)
            {
                var read = Console.ReadLine();
                if (read == "q" || read == null) break;

                Bus.Send(new ProcessOrder("DIRECT ORDER"));
            }
        }

        public void Stop()
        {
            
        }
    }
}
