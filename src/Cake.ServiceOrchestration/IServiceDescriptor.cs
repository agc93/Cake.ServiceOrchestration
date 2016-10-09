using Cake.Core.IO;

public interface IServiceDescriptor
{
    FilePath ProjectFile { get; set; }
    string Name { get; set; }
}