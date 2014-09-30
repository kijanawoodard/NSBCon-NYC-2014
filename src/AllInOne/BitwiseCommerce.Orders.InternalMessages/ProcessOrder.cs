namespace BitwiseCommerce.Orders.InternalMessages
{
    public class ProcessOrder
    {
        public string Id { get; set; }
        
        public ProcessOrder(string id)
        {
            Id = id;
        }
    }
}
