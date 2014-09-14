using System.Net;

namespace BitwiseCommerce.Api
{
    public class OrderController
    {
        private readonly IOrderRepository _orders;
        private readonly IBillingService _billing;
        private readonly IShippingService _shipping;

        public OrderController(IOrderRepository orders, IBillingService billing, IShippingService shipping)
        {
            _orders = orders;
            _billing = billing;
            _shipping = shipping;
        }

        public HttpStatusCode Post(Order order)
        {
            _orders.Save(order.OrderDetails);
            _billing.Charge(order.BillingDetails);
            _shipping.Ship(order.ShippingDetails);

            return HttpStatusCode.OK;
        }
    }

    public class Order
    {
        public OrderDetails OrderDetails { get; set; }
        public BillingDetails BillingDetails { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
    }

    public class OrderDetails
    {
    }

    public class BillingDetails
    {
    }

    public class ShippingDetails
    {
    }


    public interface IOrderRepository
    {
        int Save(OrderDetails details);
    }

    public interface IBillingService
    {
        int Charge(BillingDetails details);
    }

    public interface IShippingService
    {
        int Ship(ShippingDetails details);
    }
}
