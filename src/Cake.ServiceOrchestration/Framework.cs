using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Tools.MSBuild;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.IIS;

public interface IServiceInstance
{
    IServiceDescriptor Service { get; }
    Uri InstanceUri { get; }
    DirectoryPath RemotePath { get; }
    DirectoryPath LocalPath { get; }
}

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

    

    private void CopyWITDataStore()
    {
        var libs = _context.GetFiles("./lib/*WITDataStore*.dll");
        _context.Information("Copying {0} libraries from './lib'", libs.Count());
        try
        {
            _context.CopyFiles(libs, RemotePath + "/bin/");
        }
        catch
        {
            _context.Warning("Failed to copy WITDataStore libraries!");
        }
    }
}