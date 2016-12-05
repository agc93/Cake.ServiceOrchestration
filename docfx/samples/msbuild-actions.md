Below are some sample `IServiceAction` implementations using the built-in MSBuild aliases. You can put these in your script directly or include as a compiled reference.

The first example will invoke MSBuild to publish to the `RemotePath` of your instance. This is roughly equivalent of Visual Studio's Publish command when using the File System Publish steps.

```csharp
public class PublishApi : IServiceAction
{
    public void Run(ICakeContext ctx, IServiceInstance i)
    {
        ctx.Information("Publishing {0} to {1}...", i.Service.ProjectFile.GetFilenameWithoutExtension(),
            i.RemotePath);
        ctx.MSBuild(i.Service.ProjectFile, s => 
           s.WithTarget("WebPublish")
            .SetConfiguration("Debug")
            .SetVerbosity(Verbosity.Quiet)
            .WithProperty("WebPublishMethod", "FileSystem")
            .WithProperty("PublishUrl", i.RemotePath));
    }
}

public class CopyAppData : IServiceAction
{
    public void Run(ICakeContext ctx, IServiceInstance i)
    {
        ctx.EnsureDirectoryExists(i.RemotePath.Combine("App_Data/"));
        ctx.CopyFiles("./" + i.Service.ProjectFile.GetDirectory() + "/App_Data/*.*",
            i.RemotePath.Combine("App_Data/"));
    }
}
```

This example uses the below extension method:

```csharp
public static string Combine(this string s, string t)
{
    return System.IO.Path.Combine(s, t);
}
```