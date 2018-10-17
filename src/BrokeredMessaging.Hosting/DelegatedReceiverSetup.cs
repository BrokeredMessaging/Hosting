using System;
using BrokeredMessaging.Messaging;

namespace BrokeredMessaging.Hosting
{
    /// <summary>
    /// A setup which invokes a delegate to configure the message-receiver pipeline.
    /// </summary>
    public class DelegatedReceiverSetup : IReceiverSetup
    {
        private readonly Action<IReceiverBuilder> _configureReceiver;

        public DelegatedReceiverSetup(Action<IReceiverBuilder> configureReceiver)
        {
            _configureReceiver = configureReceiver ?? throw new ArgumentNullException(nameof(configureReceiver));
        }

        public void Configure(IReceiverBuilder receiver) => _configureReceiver(receiver);
    }
}