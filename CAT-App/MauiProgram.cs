using CAT_App.Data;
using Microsoft.Extensions.Logging;

namespace CAT_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		    builder.Services.AddBlazorWebViewDeveloperTools();
		    builder.Logging.AddDebug();
#endif

            string googleMapsApiKey = ""; // This must be replaced with a Google Maps API key with the Maps JavaScript API enabled. 

            builder.Services.AddSingleton<GoogleMapsService>(sp =>
            new GoogleMapsService(new HttpClient(), googleMapsApiKey));

            return builder.Build();
        }
    }
}