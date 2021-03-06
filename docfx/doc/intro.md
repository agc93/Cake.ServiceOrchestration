# Introduction

## History

This library was originally developed for a client looking to make their first steps into microservices. Since we had already centralised their build scripts using [Cake](http://cakebuild.net), it seemed reasonable to develop their deployment scripts and steps as Cake scripts as well. After that engagement was complete, I stripped down the library we used there and (with permission) published it here.

## Package

You can install the package in your build scripts using `#addin` as per usual, for example:

```csharp
#addin nuget:?package=Cake.ServiceOrchestration
```

> Make sure you get version 0.2 or later, which introduced a breaking change to fix a bug in 0.1.x

You can then use the `DefineService` alias and all the other types included in the library to define and orchestrate your services.

## Getting started

First read the [Getting Started](getting-started.md) guide to get a feel for how this framework is designed, or you can find basic annotated examples in the Samples documentation. To find out more about the library and how to apply it in your own projects, check the [Background](background.md) documentation.