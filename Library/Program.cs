using UI;

namespace Program
{
    class Program
    {

        static int Main(string[] args)
        {
            var lib = new Library();

            if (args.Length != 0 && args[0] == "-d")
                Debug.DebugPrefill(lib);

            var mainMenu = new MainMenu(null, lib);
            mainMenu.CommandWait();
            return 0;
        }
    }
}
