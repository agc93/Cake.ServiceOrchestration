using System;
using System.Collections.Generic;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     High-level interface for the registration and execution of service deployments
    /// </summary>
    public interface IServiceManager
    {
        /// <summary>
        ///     Gets the definition for this service.
        /// </summary>
        IServiceDescriptor Definition { get; }

        /// <summary>
        ///     Gets the list of currently registered instances for this service.
        /// </summary>
        List<IServiceInstance> Instances { get; }

        /// <summary>
        ///     Gets the service instance with the specified hostname
        /// </summary>
        /// <param name="hostname">The hostname of the of the service instance to retrieve.</param>
        /// <returns>The service instance with the specified hostname.</returns>
        IServiceInstance this[string hostname] { get; }

        /// <summary>
        ///     Reigsters a new pre-deployment action for instances of the current service.
        /// </summary>
        /// <param name="action">An action to perform before deploying this service.</param>
        /// <returns>The current service manager.</returns>
        IServiceManager RegisterSetupAction(IServiceAction action);

        /// <summary>
        ///     Registers a new deploy-time action for instances of the current service.
        /// </summary>
        /// <param name="action">An action to perform to deploy this service.</param>
        /// <returns>The current service manager.</returns>
        IServiceManager RegisterDeployAction(IServiceAction action);

        /// <summary>
        ///     Registers a new post-deployment action for instances of the current service.
        /// </summary>
        /// <param name="action">An action to perform following the deployment of this service.</param>
        /// <returns>The current service manager.</returns>
        IServiceManager RegisterConfigureAction(IServiceAction action);

        /// <summary>
        ///     Deploys all instances of the current service.
        /// </summary>
        /// <remarks>Implementations may use varying logic for this process</remarks>
        void DeployService();

        /// <summary>
        ///     Deploys all instances of the current service that match the given filter.
        /// </summary>
        /// <remarks>Implementations may use varying logic for this process</remarks>
        void DeployService(IServiceFilter filter);
    }
}