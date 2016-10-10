using System;
using System.Collections.Generic;
using Cake.Core;
using System.Linq;
using Cake.Core.Diagnostics;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     High-level class responsible for the registration and execution of service deployments
    /// </summary>
    public class ServiceManager : IServiceManager
    {
        private readonly ICakeContext _context;

        internal ServiceManager(ICakeContext ctx, IServiceDescriptor descriptor)
        {
            _context = ctx;
            Definition = descriptor;
        }

        private List<Action<ICakeContext, IServiceInstance>> SetupActions { get; } =
            new List<Action<ICakeContext, IServiceInstance>>();

        private List<Action<ICakeContext, IServiceInstance>> DeployActions { get; } =
            new List<Action<ICakeContext, IServiceInstance>>();

        private List<Action<ICakeContext, IServiceInstance>> ConfigureActions { get; } =
            new List<Action<ICakeContext, IServiceInstance>>();

        /// <summary>
        ///     Gets the service instance at the given index
        /// </summary>
        /// <param name="index">The index of the instance to get.</param>
        /// <returns>The service instance at the given index.</returns>
        public IServiceInstance this[int index] => Instances[index];

        /// <summary>
        ///     Gets the definition for this service.
        /// </summary>
        public IServiceDescriptor Definition { get; }

        /// <summary>
        ///     Gets the list of currently registered instances for this service.
        /// </summary>
        public List<IServiceInstance> Instances { get; } = new List<IServiceInstance>();


        /// <summary>
        ///     Registers a new deploy-time action for instances of the current service.
        /// </summary>
        /// <param name="action">An action to perform to deploy this service.</param>
        /// <returns>The current service manager.</returns>
        public IServiceManager RegisterDeployAction(IServiceAction action)
        {
            DeployActions.Add(action.Run);
            return this;
        }

        /// <summary>
        ///     Reigsters a new pre-deployment action for instances of the current service.
        /// </summary>
        /// <param name="action">An action to perform before deploying this service.</param>
        /// <returns>The current service manager.</returns>
        public IServiceManager RegisterSetupAction(IServiceAction action)
        {
            SetupActions.Add(action.Run);
            return this;
        }

        /// <summary>
        ///     Registers a new post-deployment action for instances of the current service.
        /// </summary>
        /// <param name="action">An action to perform following the deployment of this service.</param>
        /// <returns>The current service manager.</returns>
        public IServiceManager RegisterConfigureAction(IServiceAction action)
        {
            ConfigureActions.Add(action.Run);
            return this;
        }

        /// <summary>
        ///     Deploys all instances of the current service.
        /// </summary>
        /// <remarks>Sequentially executes the registered setup, deployment and configuration actions for each instance.</remarks>
        public void DeployService()
        {
            try
            {
                foreach (var instance in Instances)
                {
                    RunActions(SetupActions, DeployPhase.Setup, instance);
                    RunActions(DeployActions, DeployPhase.Deploy, instance);
                    RunActions(ConfigureActions, DeployPhase.Configure, instance);
                }
            }
            catch (ActionInvocationException ex)
            {
                _context.Log.Error("Exception encountered during deployment execution");
                _context.Log.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Gets the service instance with the specified hostname
        /// </summary>
        /// <param name="hostname">The hostname of the of the service instance to retrieve.</param>
        /// <returns>The service instance with the specified hostname.</returns>
        public IServiceInstance this[string hostname]
            => Instances.FirstOrDefault(i => i.InstanceUri.ToString() == hostname);

        private void RunActions(List<Action<ICakeContext, IServiceInstance>> actions, DeployPhase phase,
            IServiceInstance instance)
        {
            foreach (var action in actions)
            {
                _context.Log.Information($"Running {phase} action ({actions.IndexOf(action)} of {actions.Count})");
                try
                {
                    action.Invoke(_context, instance);
                }
                catch (Exception ex)
                {
                    throw new ActionInvocationException(phase, ex);
                }
            }
        }
    }
}