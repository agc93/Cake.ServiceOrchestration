using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;

namespace Cake.ServiceOrchestration
{
    public interface IServiceAction
    {
        void Run(ICakeContext ctx, IServiceInstance instance);
    }
}
