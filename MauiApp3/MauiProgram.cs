using Microsoft.Extensions.Logging;
using MauiApp3.Commands;
using MauiApp3.Data;
using MauiApp3.Queries;
using MauiApp3.ViewModel;

namespace MauiApp3
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
                });

            // Repozytorium graczy
            builder.Services.AddSingleton<PlayerRepository>();

            // Handlery poleceń
            builder.Services.AddTransient<AddPlayerCommandHandler>();
            builder.Services.AddTransient<DeletePlayerCommandHandler>();

            // Handlery zapytań
            builder.Services.AddTransient<GetPlayerByIdQueryHandler>();
            builder.Services.AddTransient<GetPlayersQueryHandler>();

            // ViewModel
            builder.Services.AddTransient<MainViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}