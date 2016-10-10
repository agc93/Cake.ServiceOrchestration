using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Aliases for defining services powered by the Cake.ServiceOrchestration framework
    /// </summary>
    [CakeAliasCategory("Orchestration")]
    [CakeNamespaceImport("Cake.ServiceOrchestration")]
    public static class ServiceOrchestrationAliases
    {
        /// <summary>
        ///     Defines a new service to be managed and deployed by a framework service manager.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="projectPath">Path to the service's project file.</param>
        /// <param name="serviceName">Name or identifier for the service (not instance-specific)</param>
        /// <returns>A <see cref="ServiceManager" /> manager to define and control the service.</returns>
        [CakeMethodAlias]
        public static IServiceManager DefineService(this ICakeContext ctx, FilePath projectPath, string serviceName)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            var d = new ServiceDescriptor(projectPath, serviceName);
            var mgr = new ServiceManager(ctx, d);
            return mgr;
        }
    }
}