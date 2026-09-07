#pragma warning disable CS8600, CS8602, CS8604

namespace WehrmachtEnigma
{
    public class Program
    {
        private static void Main()
        {
            WehrmachtEnigmaController controller = new WehrmachtEnigmaController();

            controller.ManualEnigma();
            //controller.JsonEnigma();
        }
    }
}