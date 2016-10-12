using System;
using System.Collections.Generic;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Represents a single instance of a managed service
    /// </summary>
    public interface IServiceInstance
    {
        /// <summary>
        ///     Gets the definition of the current service.
        /// </summary>
        IServiceDescriptor Service { get; }

        /// <summary>
        ///     Gets the URI of this instance.
        /// </summary>
        Uri InstanceUri { get; }

        /// <summary>
        ///     Gets a remotely accessible path for this instance.
        /// </summary>
        /// <remarks>May be null or empty if not remotely accessible.</remarks>
        string RemotePath { get; }

        /// <summary>
        ///     Gets the instance-local "install" path for this instance.
        /// </summary>
        /// <remarks>May be null or empty if not locally installed.</remarks>
        DirectoryPath LocalPath { get; }

        /// <summary>
        ///     Gets any tags associated with this instance.
        /// </summary>
        /// <remarks>Tags are optional and may not be populated on all instances.</remarks>
        IEnumerable<string> Tags { get; }
    }
}