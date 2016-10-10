using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Represents an identifier for a specific service (not instance-specific)
    /// </summary>
    public interface IServiceDescriptor
    {
        /// <summary>
        ///     Path to the service's project file or definition.
        /// </summary>
        FilePath ProjectFile { get; }

        /// <summary>
        ///     Name or identifier for the service. Made available to all instances of the service.
        /// </summary>
        /// <remarks>Must not be instance-specific</remarks>
        string Name { get; }
    }
}