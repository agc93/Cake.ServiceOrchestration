using Cake.Core.IO;

public interface IServiceDescriptor
{
    FilePath ProjectFile { get; set; }
    string Name { get; set; }
}

public class ServiceDescriptor : IServiceDescriptor
{
    public FilePath ProjectFile { get; set; }
    public string Name { get; set; }

    internal ServiceDescriptor(FilePath projectPath)
    {
        ProjectFile = projectPath;
        Name = projectPath.GetFilenameWithoutExtension()
            .ToString()
            .Replace("StatusApi", string.Empty)
            .Trim('.');
    }

    internal ServiceDescriptor(FilePath projectPath, string serviceName)
    {
        ProjectFile = projectPath;
        Name = serviceName;
    }
}