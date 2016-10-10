using System.Collections.Generic;

namespace Cake.ServiceOrchestration
{
    public interface IServiceManager
    {
        IServiceDescriptor Definition { get; }
        List<IServiceInstance> Instances { get; }
        IServiceInstance this[string hostname] { get; }
        IServiceManager RegisterSetupAction(IServiceAction action);
        IServiceManager RegisterDeployAction(IServiceAction action);
        IServiceManager RegisterConfigureAction(IServiceAction action);
        void DeployService();
    }
}