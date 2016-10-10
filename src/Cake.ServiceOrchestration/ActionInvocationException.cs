using System;

namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Thrown when there is an error or unhandled exception during the execution of a deployment pipeline.
    /// </summary>
    public class ActionInvocationException : Exception
    {
        /// <summary>
        ///     Creates a new instance of an <see cref="ActionInvocationException" /> for the given deployment phase.
        /// </summary>
        /// <param name="phase">The executing phase that resulted in an error.</param>
        public ActionInvocationException(DeployPhase phase)
        {
            Phase = phase;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ActionInvocationException" /> exception for the given deployment phase and
        ///     executing action.
        /// </summary>
        /// <param name="phase">The executing phase that resulted in an error.</param>
        /// <param name="action">The specific action that threw an exception.</param>
        public ActionInvocationException(DeployPhase phase, IServiceAction action) : this(phase)
        {
            FailedAction = action;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ActionInvocationException" /> exception for the given deployment phase
        ///     caused by the given exception.
        /// </summary>
        /// <param name="phase">The executing phase that resulted in an error.</param>
        /// <param name="ex">The exception that occurred during execution.</param>
        public ActionInvocationException(DeployPhase phase, Exception ex) : this(GetMessage(phase), ex)
        {
            Phase = phase;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="ActionInvocationException" /> exception for the given deployment phase
        ///     caused by the given exception being thrown from the specific action.
        /// </summary>
        /// <param name="phase">The executing phase that resulted in an error.</param>
        /// <param name="action">The specific action that threw an exception.</param>
        /// <param name="ex">The exception that occurred during execution.</param>
        public ActionInvocationException(DeployPhase phase, IServiceAction action, Exception ex)
            : this(GetMessage(phase, action), ex)
        {
            FailedAction = action;
            Phase = phase;
        }

        private ActionInvocationException(string message, Exception ex) : base(message, ex)
        {
        }

        /// <summary>
        ///     The action, if provided, which threw the unhandled exception.
        /// </summary>
        public IServiceAction FailedAction { get; }

        /// <summary>
        ///     The phase during which an exception occurred.
        /// </summary>
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