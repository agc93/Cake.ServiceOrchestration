Below are some sample `IServiceAction` implementations using the [Cake.IIS](http://nuget.org/packages/Cake.IIS) addin. You can put these in your script directly or include as a compiled reference.

```csharp
///<summary>
/// This action will create a new website with the services name, bound to the instance address.
///</summary>
public class CreateWebsiteAction : IServiceAction
{
    public void Run(ICakeContext ctx, IServiceInstance i)
    {
        if (!ctx.SiteExists(i.InstanceUri.Host, i.Service.Name))
        {
            ctx.CreateWebsite(i.InstanceUri.Host, new WebsiteSettings
            {
                Name = i.Service.Name,
                Binding = IISBindings.Http.SetHostName(i.InstanceUri.Host).SetPort(i.InstanceUri.Port),
                PhysicalDirectory = i.LocalPath.FullPath,
                ApplicationPool = new ApplicationPoolSettings
                {
                    Name = i.Service.Name
                }
            });
        }
    }
}
```

---

> This action will start the website and pool with your service's name (perfect when used with the above `CreateWebsiteAction`)

```csharp
public class StartWebsiteAction : IServiceAction
{
    public void Run(ICakeContext ctx, IServiceInstance i)
    {
        ctx.StartPool(i.InstanceUri.Host, i.Service.Name);
        ctx.StartSite(i.InstanceUri.Host, i.Service.Name);
    }
}
```

---

> This action will stop the website and pool with your service's name (perfect when used with the above `CreateWebsiteAction`) 

```csharp
public class StopWebsiteAction : IServiceAction
{
    public void Run(ICakeContext ctx, IServiceInstance i)
    {
        ctx.StopSite(i.InstanceUri.Host, i.Service.Name);
        ctx.StopPool(i.InstanceUri.Host, i.Service.Name);
    }
}
```

---

Combining these, you can define your actions like this:

```csharp
manager.RegisterSetupAction<CreateWebsiteAction>()
	.RegisterSetupAction<StopWebsiteAction>()
	.RegisterConfigureAction<StartWebsiteAction>();
```