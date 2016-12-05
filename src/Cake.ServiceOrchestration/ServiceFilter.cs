using System;
using System.Collections.Generic;
using System.Linq;

namespace Cake.ServiceOrchestration
{
    internal class ServiceFilter : IServiceFilter
    {
        private readonly Func<IServiceInstance, bool> _predicate;

        internal ServiceFilter(Func<IServiceInstance, bool> predicate)
        {
            _predicate = predicate;
        }

        public IEnumerable<IServiceInstance> GetInstances(IEnumerable<IServiceInstance> instances)
        {
            return instances.Where(_predicate);
        }
    }
}