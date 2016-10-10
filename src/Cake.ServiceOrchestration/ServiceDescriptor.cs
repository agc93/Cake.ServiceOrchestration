using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     An identifier for a generic managed service.
    /// </summary>
    public class ServiceDescriptor : IServiceDescriptor
    {
        internal ServiceDescriptor(FilePath projectPath)
        {
            ProjectFile = projectPath;
            Name = projectPath.GetFilenameWithoutExtension()
                .ToString()
                .Trim('.', ' ');
        }

        internal ServiceDescriptor(FilePath projectPath, string serviceName)
        {
            ProjectFile = projectPath;
            Name = serviceName;
        }

        /// <summary>
        ///     Path to the service's project file.
        /// </summary>
        public FilePath ProjectFile { get; set; }

        /// <summary>
        ///     Name or identifier for the service. Made available to all instances of the service.
        /// </summary>
        /// <remarks>Must not be instance-specific</remarks>
        public string Name { get; set; }
    }
}