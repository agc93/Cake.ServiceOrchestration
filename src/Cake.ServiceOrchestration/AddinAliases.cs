using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    [CakeAliasCategory("Orchestration")]
    [CakeNamespaceImport("Cake.ServiceOrchestration")]
    public static class ServiceOrchestrationAliases
    {
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