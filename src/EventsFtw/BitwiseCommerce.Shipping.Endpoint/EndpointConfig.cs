using NServiceBus;

namespace BitwiseCommerce.Shipping.Endpoint
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("InternalMessages"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Contracts"))
                .UseInMemoryTimeoutPersister()
                .InMemorySagaPersister()
                .InMemorySubscriptionStorage()
                .InMemoryFaultManagement();

            Configure.Serialization.Json();
        }
    }
}
