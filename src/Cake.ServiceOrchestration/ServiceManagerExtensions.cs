using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    public static class ServiceManagerExtensions
    {
        public static IServiceManager RegisterDeployAction(this IServiceManager manager, Action<ICakeContext, IServiceInstance> action)
        {
            manager.RegisterDeployAction(new ServiceAction(action));
            return manager;
        }

        public static IServiceManager RegisterSetupAction(IServiceManager manager, Action<ICakeContext, IServiceInstance> action)
        {
            manager.RegisterSetupAction(new ServiceAction(action));
            return manager;
        }

        public static IServiceManager RegisterConfigureAction(this IServiceManager manager, Action<ICakeContext, IServiceInstance> action)
        {
            manager.RegisterConfigureAction(new ServiceAction(action));
            return manager;
        }

        public static IServiceManager CreateInstanceFor(this IServiceManager manager, Uri uri, DirectoryPath sharePath,
            DirectoryPath localPath)
        {
            manager.Instances.Add(new ServiceInstance(manager.Definition)
            {
                InstanceUri = uri,
                LocalPath = localPath,
                RemotePath = sharePath
            });
            return manager;
        }

        public static IServiceManager CreateInstanceFor(this IServiceManager manager, string uri,
            DirectoryPath sharePath, DirectoryPath localPath)
        {
            var u = new Uri(uri.StartsWith("http://") ? uri : "http://" + uri);
            return manager.CreateInstanceFor(u, sharePath, localPath);
        }
    }
}
