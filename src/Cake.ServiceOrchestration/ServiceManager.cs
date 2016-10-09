using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;

namespace Cake.ServiceOrchestration
{
    public class ServiceManager : IServiceManager
    {
        private List<Action<ICakeContext, IServiceInstance>> SetupActions { get; } = new List<Action<ICakeContext, IServiceInstance>>();
        private List<Action<ICakeContext, IServiceInstance>> DeployActions { get; } = new List<Action<ICakeContext, IServiceInstance>>();
        private List<Action<ICakeContext, IServiceInstance>> ConfigureActions { get; } = new List<Action<ICakeContext, IServiceInstance>>();
        private readonly ICakeContext _context;

        internal ServiceManager(ICakeContext ctx, IServiceDescriptor descriptor)
        {
            _context = ctx;
            Definition = descriptor;
        }
        public IServiceDescriptor Definition { get; }
        public List<IServiceInstance> Instances { get; } = new List<IServiceInstance>();

        public IServiceManager RegisterDeployAction(IServiceAction action)
        {
            DeployActions.Add(action.Run);
            return this;
        }

        public IServiceManager RegisterSetupAction(IServiceAction action)
        {
            SetupActions.Add(action.Run);
            return this;
        }

        public IServiceManager RegisterConfigureAction(IServiceAction action)
        {
            ConfigureActions.Add(action.Run);
            return this;
        }

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

        private void RunActions(List<Action<ICakeContext, IServiceInstance>> actions, DeployPhase phase, IServiceInstance instance)
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

        public IServiceInstance this[string hostname] => Instances.FirstOrDefault(i => i.InstanceUri.ToString() == hostname);

        public IServiceInstance this[int index] => Instances[index];
    }
}
