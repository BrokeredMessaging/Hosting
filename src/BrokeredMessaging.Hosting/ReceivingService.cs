using BrokeredMessaging.Hosting.Builder;
using BrokeredMessaging.Messaging;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrokeredMessaging.Hosting
{
    /// <summary>
    /// A hosted service which receives messages from a broker.
    /// </summary>
    public class ReceivingService : IHostedService
    {
        private readonly IReceiverBuilderFactory _receiverBuilderFactory;

        private readonly IReceiverSetup _receiverSetup;

        public ReceivingService(
            IReceiverBuilderFactory receiverBuilderFactory,
            IReceiverSetup receiverSetup)
        {
            _receiverBuilderFactory = receiverBuilderFactory ?? throw new ArgumentNullException(nameof(receiverBuilderFactory));
            _receiverSetup = receiverSetup ?? throw new ArgumentNullException(nameof(receiverSetup));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var receiver = BuildReceiveDelegate();

            throw new NotImplementedException();
        }

        private ReceiveDelegate BuildReceiveDelegate()
        {
            var builder = _receiverBuilderFactory.CreateBuilder();

            _receiverSetup.Configure(builder);

            return builder.Build();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
