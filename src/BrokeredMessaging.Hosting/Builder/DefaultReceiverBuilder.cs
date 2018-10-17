using BrokeredMessaging.Messaging;
using BrokeredMessaging.Messaging.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrokeredMessaging.Hosting.Builder
{
    /// <summary>
    /// The default <see cref="IReceiverBuilder"/> implementation for hosted receive pipelines.
    /// </summary>
    public class DefaultReceiverBuilder : IReceiverBuilder
    {
        private readonly ICollection<Func<ReceiveDelegate, ReceiveDelegate>> _middleware 
            = new List<Func<ReceiveDelegate, ReceiveDelegate>>();

        private static readonly ReceiveDelegate NullReceive = ctx => Task.CompletedTask;

        public DefaultReceiverBuilder(IServiceProvider receiverServices)
        {
            ReceiverServices = receiverServices ?? throw new ArgumentNullException(nameof(receiverServices));
        }

        public IServiceProvider ReceiverServices { get; }

        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public ReceiveDelegate Build()
        {
            // Start with a "null" receive as the default/final call in the pipeline.
            var receieve = NullReceive;

            // Apply the middlewear in the reverse order, so the functions chain in the expected order.
            foreach (var middlewear in _middleware.Reverse())
            {
                receieve = middlewear(receieve);
            }

            return receieve;
        }

        public IReceiverBuilder Use(Func<ReceiveDelegate, ReceiveDelegate> middlewear)
        {
            if (middlewear == null)
            {
                throw new ArgumentNullException(nameof(middlewear));
            }

            _middleware.Add(middlewear);

            return this;
        }
    }
}
