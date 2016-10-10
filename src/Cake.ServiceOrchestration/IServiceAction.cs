using Cake.Core;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Represents a single unit of work in the deployment pipeline
    /// </summary>
    /// <remarks>Instances of this interface are registered with a service manager.</remarks>
    public interface IServiceAction
    {
        /// <summary>
        ///     Executes during one or more phases of the deployment pipeline
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <param name="instance">The current instance to deploy.</param>
        void Run(ICakeContext ctx, IServiceInstance instance);
    }
}