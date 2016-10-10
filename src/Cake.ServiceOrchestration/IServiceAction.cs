using Cake.Core;

namespace Cake.ServiceOrchestration
{
    public interface IServiceAction
    {
        void Run(ICakeContext ctx, IServiceInstance instance);
    }
}