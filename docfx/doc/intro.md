# Introduction

## History

This library was originally developed for a client looking to make their first steps into microservices. Since we had already centralised their build scripts using [Cake](http://cakebuild.net), it seemed reasonable to develop their deployment scripts and steps as Cake scripts as well. After that engagement was complete, I stripped down the library we used there and (with permission) published it here.

## Package

You can install the package in your build scripts using `#addin` as per usual, for example:

```csharp
#addin nuget:?package=Cake.ServiceOrchestration
```
