using System;
using Cake.Core;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Extensions methods for using an <see cref="IServiceManager" /> with the <see cref="CakeTaskBuilder{T}" />.
    /// </summary>
    public static class TaskBuilderExtensions
    {
        /// <summary>
        ///     Automatically Configures a task to deploy the given <see cref="IServiceManager" />.
        /// </summary>
        /// <param name="builder">The task builder.</param>
        /// <param name="serviceManager">The service manager.</param>
        /// <returns></returns>
        public static CakeTaskBuilder<ActionTask> Deploys(this CakeTaskBuilder<ActionTask> builder,
            IServiceManager serviceManager)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.Task.AddAction(ctx => serviceManager.DeployService());
            return builder;
        }

        /// <summary>
        ///     Automatically Configures a task to deploy the given <see cref="IServiceManager" /> using the given instance filter.
        /// </summary>
        /// <param name="builder">The task builder.</param>
        /// <param name="serviceManager">The service manager.</param>
        /// <param name="filter">The deployment filter.</param>
        /// <returns></returns>
        public static CakeTaskBuilder<ActionTask> Deploys(this CakeTaskBuilder<ActionTask> builder,
            IServiceManager serviceManager, Func<IServiceInstance, bool> filter)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.Task.AddAction(ctx => serviceManager.DeployService(filter));
            return builder;
        }
    }
}