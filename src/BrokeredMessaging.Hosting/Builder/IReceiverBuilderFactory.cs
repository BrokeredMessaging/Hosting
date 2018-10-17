using BrokeredMessaging.Messaging;

namespace BrokeredMessaging.Hosting.Builder
{
    /// <summary>
    /// Defines a component which can create <see cref="IReceiverBuilder"/> instances.
    /// </summary>
    public interface IReceiverBuilderFactory
    {
        /// <summary>
        /// Creates a builder.
        /// </summary>
        /// <returns>The <see cref="IReceiverBuilder"/>.</returns>
        IReceiverBuilder CreateBuilder();
    }
}
