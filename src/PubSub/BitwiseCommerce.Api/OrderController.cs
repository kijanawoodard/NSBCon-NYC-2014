using System.Net;
using BitwiseCommerce.Orders.InternalMessages;
using NServiceBus;

namespace BitwiseCommerce.Api
{
    public class OrderController
    {
        private readonly IHandleMessages<ProcessOrder> _handler;
        public IBus Bus { get; set; }

        public OrderController(IHandleMessages<ProcessOrder> handler)
        {
            _handler = handler;
        }

        public HttpStatusCode Post(ProcessOrder order)
        {
            _handler.Handle(order);
            return HttpStatusCode.OK;
        }
    }
}
