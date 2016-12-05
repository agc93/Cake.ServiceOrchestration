This sample is a basic rundown of a simple deployment pipeline for use in a Cake script (uses the `DefineService` alias)

```csharp
// Define our service using its project file and name
var service = DefineService("./src/Project.csproj", "MyService");

//Register actions using the fluent syntax and extension methods
service.RegisterSetupAction((ctx, i) => { ctx.CreateDirectory(i.RemotePath); })
    .RegisterDeployAction((ctx, i) => { ctx.Unzip("./artifacts/package.zip", i.RemotePath); })
    .RegisterConfigureAction((ctx, i) =>
    {
        ctx.XmlTransform($"./transforms/{i.InstanceUri.Host}.xslt", $"{i.RemotePath}/template.config",
            $"{i.RemotePath}/app.config");
    })
    .RegisterConfigureAction((ctx, i) =>
    {
        // Register with your service registry here, for example
        // The default service manager runs actions in the order they are registered
    });

// Define the instances of your services.
// You can define only 1 or as many as you want to deploy
service.CreateInstanceFor("http://hostname:80/path", @"\\server\application", @"E:\Services\App")
    .CreateInstanceFor("http://backup:81/path", @"\\backupserver\application", @"D:\Services\App");

// Deploy your services. This will fire the action pipeline on each defined instances
// Note that the filter syntax shown here is new in 0.3.0
service.DeployService(i => i.Tags.Any());
```