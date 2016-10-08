using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;

namespace Cake.ServiceOrchestration
{
    public interface IServiceAction
    {
        void Run(ICakeContext ctx, IServiceInstance instance);
    }

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
