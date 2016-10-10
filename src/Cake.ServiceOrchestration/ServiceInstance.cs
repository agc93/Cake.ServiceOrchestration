using System;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     An instance of a generic managed, installable service.
    /// </summary>
    public class ServiceInstance : IServiceInstance
    {
        internal ServiceInstance(IServiceDescriptor d)
        {
            Service = d;
        }

        /// <summary>
        ///     The port of the current instance.
        /// </summary>
        public int Port => InstanceUri.Port;

        /// <summary>
        ///     The hostname of the current instance.
        /// </summary>
        public string Host => InstanceUri.Host;

        /// <summary>
        ///     Gets the definition of the current service.
        /// </summary>
        public IServiceDescriptor Service { get; }

        /// <summary>
        ///     Gets the URI of this instance.
        /// </summary>
        public Uri InstanceUri { get; internal set; }

        /// <summary>
        ///     Gets a remotely accessible path for this instance.
        /// </summary>
        /// <remarks>May be null or empty if not remotely accessible.</remarks>
        public string RemotePath { get; internal set; }

        /// <summary>
        ///     Gets the instance-local "install" path for this instance.
        /// </summary>
        public DirectoryPath LocalPath { get; internal set; }
    }
}