using Cake.Core.IO;

namespace Cake.ServiceOrchestration
{
    public interface IServiceDescriptor
    {
        FilePath ProjectFile { get; set; }
        string Name { get; set; }
    }
}