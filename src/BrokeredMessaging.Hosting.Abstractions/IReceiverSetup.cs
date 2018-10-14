using BrokeredMessaging.Messaging;

namespace BrokeredMessaging.Hosting
{
    /// <summary>
    /// Represents the setup of a message-receiver.
    /// </summary>
    public interface IReceiverSetup
    {
        /// <summary>
        /// Configures an <see cref="IReceiverBuilder"/> according to the setup.
        /// </summary>
        /// <param name="receiver">The builder to configure.</param>
        void Configure(IReceiverBuilder receiver);
    }
}
