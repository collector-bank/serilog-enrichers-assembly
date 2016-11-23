# Serilog.Enrichers.Assembly

Enriches Serilog events with information from the process environment.
 
To use the enricher, first install the NuGet package:

```powershell
Install-Package Serilog.Enrichers.Assembly
```

Then, apply the enricher to you `LoggerConfiguration`:

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.With<SourceSystem<ClassInAssemblyOfSourceSystem>>()
    // ...other configuration...
    .CreateLogger();
```

### Included enrichers

The package includes:

 * `SourceSystem<T>()` - adds `SourceSystem` based the name of the assembly that T resides in.
 * `SourceSystemInformationalVersion<T>()` - adds `SourceSystemInformationalVersion` based on the assemblys informational version.