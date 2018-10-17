using BrokeredMessaging.Hosting.Builder;
using BrokeredMessaging.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;

namespace BrokeredMessaging.Hosting
{
    /// <summary>
    /// Defines extensions to the <see cref="IHostBuilder"/> interface to configure message receiving.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Specifies a function to use to configure the message receiver for the application.
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configureReceiver">The delegate to configure the <see cref="IReceiverBuilder"/>.</param>
        public static void ConfigureMessageReceiver(
            this IHostBuilder builder,
            Action<IReceiverBuilder> configureReceiver)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configureReceiver == null)
            {
                throw new ArgumentNullException(nameof(configureReceiver));
            }

            builder.AddDefaultReceiverServices();

            builder.ConfigureServices(
                (context, services) => services.AddSingleton<IReceiverSetup>(
                    new DelegatedReceiverSetup(configureReceiver)));
        }

        private static void AddDefaultReceiverServices(this IHostBuilder builder)
        {
            builder.ConfigureServices((ctx, services) =>
            {
                // Add defaults of the services; don't replace any user-specified definitions.
                services.TryAddSingleton<IReceiverBuilderFactory, DefaultReceiverBuilderFactory>();
            });          
        }

        /// <summary>
        /// Registers an action to configure messsage receiving options.
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configureAction">An action to use to configure the options.</param>
        public static void ConfigureMessageReceiving(
            this IHostBuilder builder,
            Action<MessageReceivingOptions> configureAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configureAction == null)
            {
                throw new ArgumentNullException(nameof(configureAction));
            }
        }

        /// <summary>
        /// Registers an action to configure messsage receiving options.
        /// </summary>
        /// <param name="builder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="configureAction">An action to use to configure the options.</param>
        public static void ConfigureMessageReceiving(
            this IHostBuilder builder,
            Action<HostBuilderContext, MessageReceivingOptions> configureAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configureAction == null)
            {
                throw new ArgumentNullException(nameof(configureAction));
            }

            builder.ConfigureServices((ctx, services) =>
            {
                services.AddOptions();
                services.Configure<MessageReceivingOptions>(options => configureAction(ctx, options));
            });
        }
    }
}
