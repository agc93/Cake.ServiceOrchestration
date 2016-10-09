using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Cake.Common.IO;
using Cake.Common.Xml;
using Cake.Core;

namespace Cake.ServiceOrchestration
{
    public class Sample
    {
        private ICakeContext _context { get; }

        private void DeployService()
        {
            var service = _context.DefineService("./src/Project.csproj", "MyService");
            service.RegisterSetupAction((ctx, i) => { ctx.CreateDirectory(i.RemotePath); })
                .RegisterDeployAction((ctx, i) =>
                {
                    ctx.Unzip("./artifacts/package.zip", i.RemotePath);
                })
                .RegisterConfigureAction((ctx, i) =>
                {
                    ctx.XmlTransform($"./transforms/{i.InstanceUri.Host}.xslt", $"{i.RemotePath}/template.config",
                        $"{i.RemotePath}/app.config");
                })
                .RegisterConfigureAction((ctx, i) =>
                {
                    // Register with your service registry here, for example
                });
            service.CreateInstanceFor("http://hostname:80/path", @"\\server\application", @"E:\Services\App")
                .CreateInstanceFor("http://backup:81/path", @"\\backupserver\application", @"D:\Services\App");
            service.DeployService();
        }
    }
}