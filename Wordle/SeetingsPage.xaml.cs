
namespace Wordle;
public static class AppSettings
{
    public static bool IsDarkMode
    {
        // Get and set from preferences or settings storage.
        get => Preferences.Get(nameof(IsDarkMode), false);
        set => Preferences.Set(nameof(IsDarkMode), value);
    }

    public static bool IsTimerEnabled
    {
        // Get and set from preferences or settings storage.
        get => Preferences.Get(nameof(IsTimerEnabled), false);
        set => Preferences.Set(nameof(IsTimerEnabled), value);
    }

    public static int FontSize
    {
        // Get and set from preferences or settings storage.
        get => Preferences.Get(nameof(FontSize), 12);
        set => Preferences.Set(nameof(FontSize), value);
    }
}