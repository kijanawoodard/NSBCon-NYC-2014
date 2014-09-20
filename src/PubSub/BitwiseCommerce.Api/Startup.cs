
using System;
using BitwiseCommerce.Orders.InternalMessages;
using NServiceBus;

namespace BitwiseCommerce.Api
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            var controller = new OrderController();
            controller.Bus = Bus;
            var orderNumber = 1;

            Schedule
                .Every(TimeSpan.FromSeconds(2))
                .Action(() => controller.Post(new ProcessOrder(orderNumber++.ToString())));    
        }

        public void Stop()
        {
            
        }
    }
}
