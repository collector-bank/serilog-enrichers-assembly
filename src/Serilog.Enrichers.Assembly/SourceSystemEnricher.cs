namespace Serilog.Enrichers.Assembly
{
    using System;
    using System.Reflection;

    using Serilog.Core;
    using Serilog.Events;

    [Obsolete("Use nuget package Collector.Serilog.Enrichers.Assembly instead.")]
    public class SourceSystemEnricher<T> : SourceSystemEnricher
    {
        public SourceSystemEnricher()
            : base(typeof(T).GetTypeInfo().Assembly)
        {
        }
    }

    [Obsolete("Use nuget package Collector.Serilog.Enrichers.Assembly instead.")]
    public class SourceSystemEnricher : ILogEventEnricher
    {
        private readonly string _name;

        public SourceSystemEnricher(string name)
        {
            _name = name;
        }

        public SourceSystemEnricher(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            _name = assembly.GetName().Name;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return;

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("SourceSystem", _name));

        }
    }
}