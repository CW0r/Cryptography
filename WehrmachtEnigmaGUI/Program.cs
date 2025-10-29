using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace WeGUI
{
    public class Program
    {
        private static void Main()
        {
            NativeWindowSettings nativeWindowsSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1500, 900),
                Title = "Wehrmacht Enigma Machine"
            };

            using (Window window = new Window(GameWindowSettings.Default, nativeWindowsSettings))
            {
                window.Run();
            }
        }
    }
}
