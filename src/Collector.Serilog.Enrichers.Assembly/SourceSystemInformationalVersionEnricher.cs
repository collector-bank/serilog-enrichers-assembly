namespace Collector.Serilog.Enrichers.Assembly
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using global::Serilog.Core;
    using global::Serilog.Events;

    public class SourceSystemInformationalVersionEnricher<T> : SourceSystemInformationalVersionEnricher
    {
        public SourceSystemInformationalVersionEnricher()
            :base(typeof(T).GetTypeInfo().Assembly)
        {
        }
    }
    
    public class SourceSystemInformationalVersionEnricher : ILogEventEnricher
    {
        private readonly string _version;

        public SourceSystemInformationalVersionEnricher(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            if (assembly.Location == null)
                return;

            try
            {
                
                _version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (string.IsNullOrWhiteSpace(_version))
                return;

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("SourceSystemInformationalVersion", _version));
        }
    }
}