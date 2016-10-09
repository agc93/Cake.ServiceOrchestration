# Design background

This library is intended as a blueprint or reference design for implementing basic service orchestration using Cake scripting. As such, it is important to understand the role and usage patterns for this library. There are three primary ways to use or implement this framework: out-of-the-box, with domain-specific implementations, and re-implementing the framework as a whole.

## Out of the box

Although it is intended as a blueprint, the library is actually usable out-of-the-box, by simply including it in your scripts:

```csharp
#addin nuget:?package=Cake.ServiceOrchestration
```

You can then use the code included in the samples documentation to create your own scripts to define, build and orchestrate your services. This is the simplest solution, but will only be appropriate for comparatively simple services and deployments.

## Domain-specific implementations

The library is built from the ground-up around interfaces, so that swapping out individual components should be extremely simple. For example, to control the high-level orchestration workflow, provide your own implementation of the `IServiceManager`, or to only change how your deployment process is implemented, provide a new implementation of `IServiceAction`.

### Action implementations

The `IServiceAction` interface is a convenient abstraction around a unit of work you may want to do during a deployment. The library provides extension methods to use a simple `Action<ICakeContext, IServiceInstance>`, but if your deployment logic is more complex, you may want to abstract it into an `IServiceAction`, a simple class with only one member:

```csharp
void Run(ICakeContext ctx, IServiceInstance instance);
```

If your action implementation doesn't have any constructor arguments, you (or your consumers) can even use the generic extension methods to simplify usage:

```csharp
manager.RegisterSetupAction<MySetupAction>();
```

## Framework-level implementation

Since this library is intended primarily as a reference implementation, more complex orchestration scenarios may require wholesale changes. Simply fork the repository, update the code as needed and reference your library with `#load` as usual. If your changes are portable and you think they may benefit other users, open an issue or PR on this repo and you might find your changes become part of the core blueprint.

Please contact me before publically distributing derivatives of this library just to prevent confusion and so I know what the community is building!