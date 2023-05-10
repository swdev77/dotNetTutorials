using Microsoft.Extensions.Logging;

namespace BeautyShopApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("fallingsky.otf", "Fallingsky");
                fonts.AddFont("fallingskybd.otf", "Fallingsky Bold");
                fonts.AddFont("fallingskylight.otf", "Fallingsky Light");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
