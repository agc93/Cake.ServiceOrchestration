using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    public class ServiceDescriptor : IServiceDescriptor
    {
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

        public FilePath ProjectFile { get; set; }
        public string Name { get; set; }
    }
}