using System;
using BitwiseCommerce.Billing.Contracts;
using BitwiseCommerce.Orders.Contracts;
using BitwiseCommerce.Shipping.Contracts;
using NServiceBus.Saga;

namespace BitwiseCommerce.Shipping.Endpoint
{
    public class ShippingSaga :
        Saga<ShippingSagaData>,
        IAmStartedByMessages<IOrderAccepted>,
        IAmStartedByMessages<IOrderBilled>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<IOrderAccepted>(m => m.Id).ToSaga(s => s.OrderId);
            ConfigureMapping<IOrderBilled>(m => m.Id).ToSaga(s => s.OrderId);
        }

        public void Handle(IOrderAccepted message)
        {
            Data.OrderId = message.Id;
            Data.Accepted = true;
            CheckComplete();
        }

        public void Handle(IOrderBilled message)
        {
            Data.OrderId = message.Id;
            Data.Billed = true;
            CheckComplete();
        }

        private void CheckComplete()
        {
            if (Data.Accepted && Data.Billed)
            {
                Bus.Publish<IOrderShipped>(x => x.Id = Data.OrderId);
                Console.WriteLine("Order {0} shipped.", Data.OrderId);
            }
        }
    }

    public class ShippingSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string OrderId { get; set; }
        public bool Accepted { get; set; }
        public bool Billed { get; set; }
    }
}