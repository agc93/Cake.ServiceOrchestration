# 0.1.0

- Initial release

# 0.2.0

- Fix for incorrect logging message
- Changed `IServiceInstance.RemotePath` to `string` to support non-local paths

# 0.3.0

- Introduces tags for instances
- Introduces support for instance filtering when deploying (using `IServiceFilter`)
- Introduces new `Deploys(IServiceManager)` extension to simplify Task declarations