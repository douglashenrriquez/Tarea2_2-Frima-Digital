using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Graphics.Internals;
using Syncfusion.Maui.SignaturePad;

namespace Tarea2_2
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit() 
                .ConfigureMauiHandlers(handlers =>
                {
                   
                    handlers.AddHandler<SfSignaturePad, SignaturePadHandler>();
                });


            builder.Logging.AddDebug();


            return builder.Build();
        }

    }
}
