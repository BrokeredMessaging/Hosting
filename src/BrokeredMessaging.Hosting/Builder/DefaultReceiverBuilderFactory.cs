using System;
using System.Collections.Generic;
using System.Text;
using BrokeredMessaging.Messaging;
using BrokeredMessaging.Messaging.Features;

namespace BrokeredMessaging.Hosting.Builder
{
    public class DefaultReceiverBuilderFactory : IReceiverBuilderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultReceiverBuilderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IReceiverBuilder CreateBuilder() => new DefaultReceiverBuilder(_serviceProvider);
    }
}
