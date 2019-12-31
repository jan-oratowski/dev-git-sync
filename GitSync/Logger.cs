using System;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;

namespace GitSync
{
    internal static class Logger
    {
        private static TelemetryClient _telemetryClient;

        public static void TrackEvent(string content)
        {
            Console.WriteLine($"{DateTime.Now} : {content}");
            _telemetryClient?.TrackEvent(content);
        }
        public static void TrackError(Exception ex)
        {
            Console.WriteLine($"{DateTime.Now} : {ex.Message}");
            _telemetryClient?.TrackException(ex);
        }

        public static void InitAppInsights(string instrumentationKey)
        {
            var configuration = TelemetryConfiguration.CreateDefault();

            configuration.InstrumentationKey = instrumentationKey;
            configuration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

            _telemetryClient = new TelemetryClient(configuration);
        }

        public static void CloseAppInsights()
        {
            if (_telemetryClient == null)
                return;

            _telemetryClient.TrackEvent("Exiting service");

            // before exit, flush the remaining data
            _telemetryClient.Flush();

            // flush is not blocking so wait a bit
            Task.Delay(5000).Wait();
        }

        private static DependencyTrackingTelemetryModule InitializeDependencyTracking(TelemetryConfiguration configuration)
        {
            var module = new DependencyTrackingTelemetryModule();

            // prevent Correlation Id to be sent to certain endpoints. You may add other domains as needed.
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.windows.net");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.chinacloudapi.cn");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.cloudapi.de");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.usgovcloudapi.net");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("localhost");
            module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("127.0.0.1");

            // enable known dependency tracking, note that in future versions, we will extend this list. 
            // please check default settings in https://github.com/microsoft/ApplicationInsights-dotnet-server/blob/develop/WEB/Src/DependencyCollector/DependencyCollector/ApplicationInsights.config.install.xdt

            module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.ServiceBus");
            module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.EventHubs");

            // initialize the module
            module.Initialize(configuration);

            return module;
        }
    }
}
