using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    [CakeAliasCategory("Sample")]
    public static class AddinAliases
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
