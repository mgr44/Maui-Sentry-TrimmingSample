using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;

namespace SentryPOC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureEssentials()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseSentry(options =>
                {
                    options.Dsn = "<Your DSN here>";
                    options.Release = "releaseString";
                    options.Distribution = "distributionString";
                    options.Environment = "environmentString";
                    options.Debug = true;

                    options.FailedRequestStatusCodes = new List<HttpStatusCodeRange>() {};
                })
                .Logging.ClearProviders().AddNLog();

            ISetupBuilder setup = LogManager.Setup();
            setup.RegisterMauiLog();
            setup.LoadConfiguration(Configure);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void Configure(ISetupLoadConfigurationBuilder configBuilder)
        {
            configBuilder.Configuration.AddSentry(options =>
            {
                options.InitializeSdk = false;
                options.MinimumBreadcrumbLevel = NLog.LogLevel.Info;
                options.MinimumEventLevel = NLog.LogLevel.Error;
            });
        }
    }
}
