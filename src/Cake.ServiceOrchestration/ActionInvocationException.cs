using System;

namespace Cake.ServiceOrchestration
{
    public class ActionInvocationException : Exception
    {
        public ActionInvocationException(DeployPhase phase)
        {
            Phase = phase;
        }

        public ActionInvocationException(DeployPhase phase, IServiceAction action) : this(phase)
        {
            FailedAction = action;
        }

        public ActionInvocationException(DeployPhase phase, Exception ex) : this(GetMessage(phase), ex)
        {
            Phase = phase;
        }

        public ActionInvocationException(DeployPhase phase, IServiceAction action, Exception ex)
            : this(GetMessage(phase, action), ex)
        {
            FailedAction = action;
            Phase = phase;
        }

        private ActionInvocationException(string message, Exception ex) : base(message, ex)
        {
        }

        public IServiceAction FailedAction { get; }

        public DeployPhase Phase { get; }

        private static string GetMessage(DeployPhase phase)
        {
            return $"Deployment failed during {phase} phase";
        }

        private static string GetMessage(DeployPhase phase, IServiceAction action)
        {
            return $"Deployment failed during {phase} phase while executing {action.GetType().FullName}";
        }
    }
}