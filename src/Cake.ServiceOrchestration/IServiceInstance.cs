using System;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    public interface IServiceInstance
    {
        IServiceDescriptor Service { get; }
        Uri InstanceUri { get; }
        DirectoryPath RemotePath { get; }
        DirectoryPath LocalPath { get; }
    }
}