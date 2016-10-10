namespace Cake.ServiceOrchestration
{
    /// <summary>
    ///     Defines the current phase of the deployment pipeline.
    /// </summary>
    public enum DeployPhase
    {
        /// <summary>
        ///     The definition phase.
        /// </summary>
        /// <remarks>Used when the pipeline has not started.</remarks>
        Definition,

        /// <summary>
        ///     The setup or pre-deployment phase.
        /// </summary>
        Setup,

        /// <summary>
        ///     The deployment phase.
        /// </summary>
        Deploy,

        /// <summary>
        ///     The configuration or post-deployment phase.
        /// </summary>
        Configure
    }
}