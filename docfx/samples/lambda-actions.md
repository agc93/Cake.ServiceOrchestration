Since the extension methods provided in `Cake.ServiceOrchestration` allow for registering an `Action<ICakeContext, IServiceInstance>` as if it was a full `IServiceAction`, you can also simplify your code by providing lambdas as actions:

```csharp
service.RegisterSetupAction((ctx, i) => { ctx.CreateDirectory(i.RemotePath); })
    .RegisterDeployAction((ctx, i) => { ctx.Unzip("./artifacts/package.zip", i.RemotePath); });
```

Plus, since lambdas are still objects you can also reuse them just as easily as a full implementation.

```csharp
Action<ICakeContext, IServiceInstance> ConfigUpdate = (ctx, i) => {
	var configPath = i.RemotePath.Combine("web.config");
	ctx.Information("Running transform on {0} for service {1} at {2}", configPath, i.Service.Name, i.InstanceUri.ToString());
	ctx.XmlPoke(configPath,
		"/configuration/appSettings/add[@key='" + i.Service.Name + "']/@value",
		i.InstanceUri.ToString());
};

service1.RegisterConfigureAction(ConfigUpdate);
service2.RegisterConfigureAction(ConfigUpdate);
```