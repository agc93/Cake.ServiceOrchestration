# Error Handling during deployments

The error handling during the execution of a deployment pipeline is controlled by the `IServiceManager` being used, since it is part of the execution process.

The default `ServiceManager` (the type returned by the `DefineService` alias) has a simple, but fragile approach to error handling. If *any* action during *any* phase of the pipeline throws *any* unhandled exception, the pipeline will stop immediately and the manager will throw an `ActionInvocationException`.

> If you are debugging your script, the exception itself may include more information such as which action failed or what exception was thrown.

This design is intended to prevent broken deployments causing more damage than necessary, so it is recommended to place your "brittle" actions as early in the pipeline as possible, and your "dangerous" actions as late as possible. This way, a failure in a brittle component will stop the execution before your dangerous action is run, potentially damaging your environment.

As with all parts of this framework, providing your own `IServiceManager` implementation will allow you complete control over the execution phase of the pipeline, including how you handle exceptions from individual actions.

## Errors in actions

Note that this means your exceptions should **always** throw an exception if there is an error or problem! Never rely on warnings or on generic `catch`-all clauses to suppress errors as the next action to run may be dangerous to a deployment environment!