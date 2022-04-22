using H.NotifyIcon.EfficiencyMode;

namespace H.NotifyIcon;

/// <summary>
/// Provides the most useful extensions to Window in the context of using TrayIcon.
/// </summary>
[CLSCompliant(false)]
public static class WindowExtensions
{
    /// <summary>
    /// Hides the window and optionally enables the Efficiency Mode for the current process.
    /// </summary>
    /// <returns></returns>
    public static void Hide(
        this Window window,
        bool enableEfficiencyMode = true)
    {
        window = window ?? throw new ArgumentNullException(nameof(window));

#if HAS_WPF
        window.Hide();
#elif !HAS_UNO
        WindowUtilities.HideWindow(WindowNative.GetWindowHandle(window));
#endif
        if (enableEfficiencyMode &&
            Environment.OSVersion.Platform == PlatformID.Win32NT &&
            Environment.OSVersion.Version >= new Version(6, 2))
        {
#pragma warning disable CA1416 // Validate platform compatibility
            EfficiencyModeUtilities.SetEfficiencyMode(true);
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }

    /// <summary>
    /// Shows the window and optionally disables the Efficiency Mode for the current process.
    /// </summary>
    /// <returns></returns>
    public static void Show(
        this Window window,
        bool disableEfficiencyMode = true)
    {
        window = window ?? throw new ArgumentNullException(nameof(window));

#if HAS_WPF
        window.Show();
#elif !HAS_UNO
        WindowUtilities.ShowWindow(WindowNative.GetWindowHandle(window));
#endif
        if (disableEfficiencyMode &&
            Environment.OSVersion.Platform == PlatformID.Win32NT &&
            Environment.OSVersion.Version >= new Version(6, 2))
        {
#pragma warning disable CA1416 // Validate platform compatibility
            EfficiencyModeUtilities.SetEfficiencyMode(false);
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
