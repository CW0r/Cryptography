using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace WeGUI
{
    public class Program
    {
        private static void Main()
        {
            NativeWindowSettings nativeWindowsSettings = new NativeWindowSettings()
            {
                WindowState = WindowState.Maximized,
                Title = "Wehrmacht Enigma Machine",
                ClientSize = new Vector2i(1500, 900)
            };

            using (Window window = new Window(GameWindowSettings.Default, nativeWindowsSettings))
            {
                window.Run();
            }
        }
    }
}
