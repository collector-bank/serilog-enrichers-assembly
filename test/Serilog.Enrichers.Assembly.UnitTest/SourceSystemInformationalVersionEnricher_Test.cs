namespace Serilog.Enrichers.Assembly.UnitTest
{
    using System.Reflection;

    using NUnit.Framework;

    using Serilog.Events;

    [TestFixture]
    public class SourceSystemInformationalVersionEnricher_Test
    {
        [Test]
        public void When_using_the_generic_version_the_assembly_name_of_the_type_is_used()
        {
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With<SourceSystemInformationalVersionEnricher<SourceSystemEnricher_Test>>()
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("3.2.1.0", ((ScalarValue)logEvent.Properties["SourceSystemInformationalVersion"]).Value);
        }

        [Test]
        public void It_takes_the_assembly_name_from_the_supplied_assembly()
        {
            LogEvent logEvent = null;
            var logger = new LoggerConfiguration()
                .Enrich.With(new SourceSystemInformationalVersionEnricher(typeof(SourceSystemEnricher_Test).GetTypeInfo().Assembly))
                .WriteTo.Sink(new DelegatingSink(e => logEvent = e))
                .CreateLogger();

            logger.Information("Test log");

            Assert.AreEqual("3.2.1.0", ((ScalarValue)logEvent.Properties["SourceSystemInformationalVersion"]).Value);
        }
    }
}