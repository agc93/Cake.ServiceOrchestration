using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Extension methods for the <see cref="IServiceManager" /> interface.
    /// </summary>
    public static class ServiceManagerExtensions
    {
        /// <summary>
        ///     Reigsters a new pre-deployment action for instances of the current service.
        /// </summary>
        /// <param name="manager">The current service manager.</param>
        /// <param name="action">A simple action lambda to perform before deploying this service.</param>
        /// <returns>The current service manager.</returns>
        public static IServiceManager RegisterSetupAction(this IServiceManager manager,
            Action<ICakeContext, IServiceInstance> action)
        {
            manager.RegisterSetupAction(new ServiceAction(action));
            return manager;
        }

        /// <summary>
        ///     Registers a new deploy-time action for instances of the current service.
        /// </summary>
        /// <param name="manager">The current service manager.</param>
        /// <param name="action">A simple action lambda to perform to deploy this service.</param>
        /// <returns>The current service manager.</returns>
        public static IServiceManager RegisterDeployAction(this IServiceManager manager,
            Action<ICakeContext, IServiceInstance> action)
        {
            manager.RegisterDeployAction(new ServiceAction(action));
            return manager;
        }

        /// <summary>
        ///     Registers a new post-deployment action for instances of the current service.
        /// </summary>
        /// <param name="manager">The current service manager.</param>
        /// <param name="action">A simple action lambda to perform following the deployment of this service.</param>
        /// <returns>The current service manager.</returns>
        public static IServiceManager RegisterConfigureAction(this IServiceManager manager,
            Action<ICakeContext, IServiceInstance> action)
        {
            manager.RegisterConfigureAction(new ServiceAction(action));
            return manager;
        }

        /// <summary>
        ///     Reigsters an <see cref="IServiceAction" /> implementation as a pre-deployment action for instances of the current
        ///     service.
        /// </summary>
        /// <typeparam name="T">The <see cref="IServiceAction" /> implementation to register.</typeparam>
        /// <param name="manager">The current service manager.</param>
        /// <returns>The current service manager.</returns>
        public static IServiceManager RegisterSetupAction<T>(this IServiceManager manager)
            where T : IServiceAction, new()
        {
            manager.RegisterSetupAction(new T());
            return manager;
        }

        /// <summary>
        ///     Reigsters an <see cref="IServiceAction" /> implementation as a deployment action for instances of the current
        ///     service.
        /// </summary>
        /// <typeparam name="T">The <see cref="IServiceAction" /> implementation to register.</typeparam>
        /// <param name="manager">The current service manager.</param>
        /// <returns>The current service manager.</returns>
        public static IServiceManager RegisterDeployAction<T>(this IServiceManager manager)
            where T : IServiceAction, new()
        {
            manager.RegisterDeployAction(new T());
            return manager;
        }

        /// <summary>
        ///     Reigsters an <see cref="IServiceAction" /> implementation as a post-deployment action for instances of the current
        ///     service.
        /// </summary>
        /// <typeparam name="T">The <see cref="IServiceAction" /> implementation to register.</typeparam>
        /// <param name="manager">The current service manager.</param>
        /// <returns>The current service manager.</returns>
        public static IServiceManager RegisterConfigureAction<T>(this IServiceManager manager)
            where T : IServiceAction, new()
        {
            manager.RegisterConfigureAction(new T());
            return manager;
        }

        /// <summary>
        ///     Creates a new instance of the current service using the given URI, remote path and local path.
        /// </summary>
        /// <param name="manager">The current service manager.</param>
        /// <param name="uri">URI of the new instance.</param>
        /// <param name="sharePath">Remotely accessible path for the new instance.</param>
        /// <param name="localPath">Local install path for the new instance.</param>
        /// <returns>The current service manager.</returns>
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

        /// <summary>
        ///     Creates a new instance of the current service using the given URI, remote path and local path.
        /// </summary>
        /// <param name="manager">The current service manager.</param>
        /// <param name="uri">URI of the new instance (will be parsed to a <see cref="System.Uri" />).</param>
        /// <param name="sharePath">Remotely accessible path for the new instance.</param>
        /// <param name="localPath">Local install path for the new instance.</param>
        /// <returns>The current service manager.</returns>
        public static IServiceManager CreateInstanceFor(this IServiceManager manager, string uri,
            DirectoryPath sharePath, DirectoryPath localPath)
        {
            var u = new Uri(uri.StartsWith("http://") ? uri : "http://" + uri);
            return manager.CreateInstanceFor(u, sharePath, localPath);
        }
    }
}