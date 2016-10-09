using System;
using Cake.Core;

namespace Cake.ServiceOrchestration
{
    internal class ServiceAction : IServiceAction
    {
        private readonly Action<ICakeContext, IServiceInstance> _action;

        public ServiceAction(Action<ICakeContext, IServiceInstance> action)
        {
            _action = action;
        }

        public void Run(ICakeContext ctx, IServiceInstance instance)
        {
            _action.Invoke(ctx, instance);
        }
    }
}