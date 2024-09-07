namespace StackAndQueue
{
    public class CommandLineInterface()
    {
        private static readonly int borderLength = 40;
        private static readonly string[] menus = ["List", "Add", "Use", "Exit"];

        private static bool isRunning = true;
        private static int currentMenu = 0;

        public static void Start()
        {
            ConsoleKey key;
            while (isRunning)
            {
                Console.Clear();
                Border();
                Title("Command Line Interface");
                Border();

                for (int index = 0; index < menus.Length; index++)
                {
                    if (index == currentMenu)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"> {menus[index]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menus[index]}");
                    };
                };

                Border();

                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentMenu = (currentMenu == 0) ? (menus.Length - 1) : (currentMenu - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        currentMenu = (currentMenu == menus.Length - 1) ? 0 : (currentMenu + 1);
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        HandleMenu();
                        break;

                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;

                };
            };
        }

        private static void Border()
        {
            Console.WriteLine(String.Concat(Enumerable.Repeat("=", borderLength)));
        }

        private static void Title(string text)
        {
            int length = (borderLength - text.Length) / 2;

            Console.Write(String.Concat(Enumerable.Repeat("-", ((text.Length % 2) == 0) ? length : (length + 1))));
            Console.Write(text);
            Console.WriteLine(String.Concat(Enumerable.Repeat("-", length)));
        }

        private static void HandleMenu()
        {
            switch (currentMenu)
            {
                // case 0:
                //     HandleMenuList();
                //     break;

                // case 1:
                //     HandleMenuAdd();
                //     break;

                // case 2:
                //     HandleMenuUse();
                //     break;

                case 3:
                    isRunning = false;
                    break;
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CommandLineInterface.Start();
        }

        public static void TestStack()
        {
            Structures.Stack<int> stack1 = new();
            stack1.Push(1);
            stack1.Push(4);
            stack1.Push(8);

            Console.WriteLine("Show:");
            stack1.Show();
            Console.WriteLine();

            Console.WriteLine("Debug:");
            stack1.Debug();
            Console.WriteLine();

            Console.WriteLine("Swap (0, 2):");
            stack1.Show();
            stack1.Swap(0, 2);
            stack1.Show();
            Console.WriteLine();

            Console.WriteLine("Pop:");
            stack1.Show();
            Console.WriteLine($"HasPop: {stack1.HasPop()}");
            Console.WriteLine($"Pop: {stack1.Pop()}");
            stack1.Show();
            Console.WriteLine();

            Console.WriteLine("Pop:");
            stack1.Show();
            Console.WriteLine($"Peek: {stack1.Peek()}");
            stack1.Show();
            Console.WriteLine();

            Console.WriteLine("Clear:");
            stack1.Show();
            stack1.Clear();
            stack1.Show();
            Console.WriteLine();

            // Akan error, karena Stack sudah kosong
            Console.WriteLine("Pop (Memang akan sengaja error):");
            stack1.Show();
            Console.WriteLine($"HasPop: {stack1.HasPop()}");
            Console.WriteLine($"Pop: {stack1.Pop()}");
            stack1.Show();
            Console.WriteLine();
        }
    }
}