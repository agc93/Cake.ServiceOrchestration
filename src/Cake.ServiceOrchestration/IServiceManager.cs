using System.Collections.Generic;

namespace Cake.ServiceOrchestration
{
    public interface IServiceManager
    {
        IServiceDescriptor Definition { get; }
        List<IServiceInstance> Instances { get; }
        IServiceManager RegisterDeployAction(IServiceAction action);
        IServiceManager RegisterSetupAction(IServiceAction action);
        IServiceManager RegisterConfigureAction(IServiceAction action);
        void DeployService();
        IServiceInstance this[string hostname] { get; }
    }
}