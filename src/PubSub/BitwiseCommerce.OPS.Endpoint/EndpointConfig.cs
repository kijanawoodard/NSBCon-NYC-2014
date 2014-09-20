
using NServiceBus.RavenDB;

namespace BitwiseCommerce.OPS.Endpoint
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .RavenDBStorage("RavenDB")
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains("InternalMessages"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains("Contracts"))
                .UseInMemoryTimeoutPersister()
                .InMemorySagaPersister()
                .UseRavenDBSubscriptionStorage()
                .InMemoryFaultManagement();

            Configure.Serialization.Json();
        }
    }
}
