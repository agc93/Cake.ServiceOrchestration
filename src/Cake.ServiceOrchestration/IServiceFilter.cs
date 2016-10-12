using System.Collections.Generic;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    /// Represents a filter for deployed services.
    /// </summary>
    public interface IServiceFilter
    {
        /// <summary>
        /// Filters the list of available instances.
        /// </summary>
        /// <param name="instances">The currently available (registered) instances</param>
        /// <returns>The filtered list of instances to deploy.</returns>
        IEnumerable<IServiceInstance> GetInstances(IEnumerable<IServiceInstance> instances);
    }
}