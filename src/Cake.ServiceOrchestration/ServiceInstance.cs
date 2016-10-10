using System;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    public class ServiceInstance : IServiceInstance
    {
        internal ServiceInstance(IServiceDescriptor d)
        {
            Service = d;
        }

        public int Port => InstanceUri.Port;
        public string Host => InstanceUri.Host;
        public IServiceDescriptor Service { get; }
        public Uri InstanceUri { get; internal set; }
        public DirectoryPath RemotePath { get; internal set; }
        public DirectoryPath LocalPath { get; internal set; }
    }
}