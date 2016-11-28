# Getting started with `Cake.ServiceOrchestration`

Once you have included the addin in your script, the basic workflow is below:

## 1. Define your service

The addin includes a single alias: `DefineService`. This addin returns an `IServiceManager` instance (actually an instance of the default `ServiceManager` implementation) that you can use to orchestrate your services. The normal workflow would have you calling `DefineService` once for each *service* you're deploying.

```csharp
var service = DefineService("./src/Project.csproj", "MyService");
```

## 2. Register your actions

Using the instance you got from defining the service in the last step (i.e. `service`), you can now register your build and deployment actions. The framework uses a three-stage deployment pipelines:

*  **Setup actions** are run first and should be used for pre-deploy steps.
*  **Deploy actions** are run next and are intended for the main deployment process.
*  **Configure actions** are run last and are intended for post-build or configuration steps.

> The default `ServiceManager` runs all setup actions sequentially in the order they are registered, followed by deploy actions and configure actions.

#### Extension methods

The addin library includes some extension methods that make registering an action easier by accepting a simple `Action<ICakeContext, IServiceInstance` delegate/lambda. Combine that with method chaining and you get powerful expressive code like below.

```csharp
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
```

> You can also provide an `IServiceAction` implementation to abstract this logic.

## 3. Create/define your instances

Now define as many instances of your service as you need. There are two ways to do this:

#### Using the extension methods

You can use the extension methods to quickly define a new instance, providing the service URI, a remote path for the application (for use in the script itself), and a local path (for service configuration):

```csharp
service.CreateInstanceFor("http://hostname:80/path", @"\\server\application", @"E:\Services\App");
```

This will create and add a new instance of your service to the service manager (but won't deploy it yet).

> You can chain multiple calls to `CreateInstanceFor` to quickly create multiple instances.

#### Direct creation

You can add instances of your custom `IServiceInstance` implementation directly to your service manager's `Instances` property:

```csharp
service.Instances.Add(new MyServiceInstance());
```

> This is only available when using a custom `IServiceInstance`.

## 4. Deploy your services

Now, time for the big one:

```csharp
service.DeployService();
```

This one statement will automatically kick off your service manager's deployment logic, running your setup, deploy and configure actions on the instances you have defined.

### Filtering

> Filtering is only available in 0.3 and later

You can filter the instances you deploy by providing an `IServiceFilter` implementation, and your service manager will only deploy the instances returned by your filter.

The library includes an extension method for `DeployService` that takes a LINQ-like `Func<IServiceInstance, bool>` predicate, so you can also filter using the following syntax:

```csharp
service.DeployService(i => i.Tags.Contains("Stable"));
```

### Task Extension

> Task extensions are only available in 0.3 and later

You can also use a new extension method to simplify "deploy-only" task declarations. Using this lets you simplify the following code:

```csharp
Task("Service-Test")
.WithCriteria(() => service != null)
.Does(() => {
	service.DeployService();
});
```

into this shorter syntax, with or without filtering:

```csharp
Task("Deploy-Service")
.Deploys(service);

Task("Deploy-Stable")
.Deploys(service, i => i.Tags.Contains("Stable"));
```

## Summary

That's it! The general design is: define, register actions, create instances, deploy. 