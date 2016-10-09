using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.MSBuild;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

public class ServiceInstance : IServiceInstance
{

    internal ServiceInstance(IServiceDescriptor d)
    {
        Service = d;
    }
    public IServiceDescriptor Service { get; }
    public Uri InstanceUri { get; internal set; }
    public DirectoryPath RemotePath { get; internal set; }
    public DirectoryPath LocalPath { get; internal set; }
    public int Port => InstanceUri.Port;
    public string Host => InstanceUri.Host;
}