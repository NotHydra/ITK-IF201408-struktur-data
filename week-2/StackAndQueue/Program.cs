﻿namespace StackAndQueue
{
    public class CommandLineInterface()
    {
        private static readonly int borderLength = 60;
        private static readonly string[] menus = ["List", "Add", "Use", "Exit"];
        private static readonly string[] menuAddDataStructures = ["LinkedListSingly", "LinkedListDoubly", "Stack", "Back"];
        private static readonly string[] menuAddDataTypes = ["int", "char", "Back"];
        private static readonly List<object> containers = [];

        private static bool isRunning = true;
        private static int currentMenu = 0;
        private static ConsoleKey key;

        public static void Start()
        {
            FillContainer();

            while (isRunning)
            {
                Console.Clear();
                Border();
                Title("Data Structure Manager");
                Border();
                Console.WriteLine("Choose a menu:");
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
                        HandleMenu();
                        break;

                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;
                };
            };

            Title("Thank You For Using Our Program");
            Border();
        }

        private static void FillContainer()
        {
            Structures.LinkedListSingly<int> linkedListSingly1 = new();
            linkedListSingly1.Add(1);
            linkedListSingly1.Add(2);
            linkedListSingly1.Add(3);
            containers.Add(linkedListSingly1);

            Structures.LinkedListSingly<char> linkedListSingly2 = new();
            linkedListSingly2.Add('x');
            linkedListSingly2.Add('y');
            linkedListSingly2.Add('z');
            containers.Add(linkedListSingly2);

            Structures.LinkedListDoubly<int> linkedListDoubly1 = new();
            linkedListDoubly1.Add(7);
            linkedListDoubly1.Add(8);
            linkedListDoubly1.Add(9);
            containers.Add(linkedListDoubly1);

            Structures.LinkedListDoubly<char> linkedListDoubly2 = new();
            linkedListDoubly2.Add('a');
            linkedListDoubly2.Add('b');
            linkedListDoubly2.Add('c');
            containers.Add(linkedListDoubly2);

            Structures.Stack<int> stack1 = new();
            stack1.Push(7);
            stack1.Push(8);
            stack1.Push(9);
            containers.Add(stack1);

            Structures.Stack<char> stack2 = new();
            stack2.Push('a');
            stack2.Push('b');
            stack2.Push('c');
            containers.Add(stack2);
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
                case 0:
                    HandleMenuList();
                    break;

                case 1:
                    HandleMenuAdd();
                    break;

                // case 2:
                //     HandleMenuUse();
                //     break;

                case 3:
                    isRunning = false;
                    break;
            };
        }

        private static void HandleMenuList()
        {
            while (true)
            {
                Console.Clear();
                Border();
                Title("List Menu");
                Border();

                for (int index = 0; index < containers.Count; index++)
                {
                    if (containers[index] is Structures.LinkedListSingly<int>)
                    {
                        Console.WriteLine($"{index + 1}. {containers[index]} ( LinkedListSingly<int> )");
                    }
                    else if (containers[index] is Structures.LinkedListSingly<char>)
                    {
                        Console.WriteLine($"{index + 1}. {containers[index]} ( LinkedListSingly<char> )");
                    }
                    else if (containers[index] is Structures.LinkedListDoubly<int>)
                    {
                        Console.WriteLine($"{index + 1}. {containers[index]} ( LinkedListDoubly<int> )");
                    }
                    else if (containers[index] is Structures.LinkedListDoubly<char>)
                    {
                        Console.WriteLine($"{index + 1}. {containers[index]} ( LinkedListDoubly<char> )");
                    }
                    else if (containers[index] is Structures.Stack<int>)
                    {
                        Console.WriteLine($"{index + 1}. {containers[index]} ( Stack<int> )");
                    }
                    else if (containers[index] is Structures.Stack<char>)
                    {
                        Console.WriteLine($"{index + 1}. {containers[index]} ( Stack<char> )");
                    };
                };

                Border();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("> Back");
                Console.ResetColor();
                Border();

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter || key == ConsoleKey.Escape)
                {
                    break;
                };
            };
        }

        private static void HandleMenuAdd()
        {
            int currentDataStructure = 0;
            while (true)
            {
                Console.Clear();
                Border();
                Title("Add Menu");
                Border();
                Console.WriteLine("Choose a data structure:");
                Border();

                for (int index = 0; index < menuAddDataStructures.Length; index++)
                {
                    if (index == currentDataStructure)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"> {menuAddDataStructures[index]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuAddDataStructures[index]}");
                    };
                };

                Border();

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    currentDataStructure = (currentDataStructure == 0) ? (menuAddDataStructures.Length - 1) : (currentDataStructure - 1);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    currentDataStructure = (currentDataStructure == menuAddDataStructures.Length - 1) ? 0 : (currentDataStructure + 1);
                }
                else if (key == ConsoleKey.Enter)
                {
                    // HandleMenuAddChoose();
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                };
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CommandLineInterface.Start();
        }

        public static void TestLinkedListSingly()
        {
            Structures.LinkedListSingly<char> linkedListSingly1 = new();
            linkedListSingly1.Add('x');
            linkedListSingly1.Add('y');
            linkedListSingly1.Add('z');

            Console.Write("Show: ");
            linkedListSingly1.Show();
            Console.WriteLine();

            linkedListSingly1.Debug();
            Console.WriteLine();

            Console.WriteLine($"Length: {linkedListSingly1.Length()}");
            Console.WriteLine();

            Console.WriteLine($"First: {linkedListSingly1.First}");
            Console.WriteLine();

            Console.WriteLine($"Get 0: {linkedListSingly1.Get(0)}");
            Console.WriteLine($"Get 1: {linkedListSingly1.Get(1)}");
            Console.WriteLine($"Get 2: {linkedListSingly1.Get(2)}");
            Console.WriteLine();

            Console.Write($"Pop: {linkedListSingly1.Pop()} => ");
            linkedListSingly1.Show();
            Console.WriteLine();

            Console.Write("Insert: ");
            linkedListSingly1.Insert('c', 2);
            linkedListSingly1.Insert('b', 1);
            linkedListSingly1.Insert('a', 0);
            linkedListSingly1.Show();
            Console.WriteLine();

            Console.Write("Remove: ");
            linkedListSingly1.Remove(4);
            linkedListSingly1.Remove(3);
            linkedListSingly1.Remove(2);
            linkedListSingly1.Show();
            Console.WriteLine();

            Console.Write("Swap: ");
            linkedListSingly1.Swap(0, 1);
            linkedListSingly1.Show();
            Console.WriteLine();

            Console.Write("ToString: ");
            Console.WriteLine(linkedListSingly1);
            Console.WriteLine();

            linkedListSingly1.Debug();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

        }

        public static void TestLinkedListDoubly()
        {
            Structures.LinkedListDoubly<int> linkedListDoubly1 = new();
            linkedListDoubly1.Add(7);
            linkedListDoubly1.Add(8);
            linkedListDoubly1.Add(9);

            Console.Write("Show: ");
            linkedListDoubly1.Show();
            Console.WriteLine();

            linkedListDoubly1.Debug();
            Console.WriteLine();

            Console.WriteLine($"Length: {linkedListDoubly1.Length()}");
            Console.WriteLine();

            Console.WriteLine($"First: {linkedListDoubly1.First?.Value}");
            Console.WriteLine($"Last: {linkedListDoubly1.Last?.Value}");
            Console.WriteLine();

            Console.WriteLine($"Get 0: {linkedListDoubly1.Get(0)}");
            Console.WriteLine($"Get 1: {linkedListDoubly1.Get(1)}");
            Console.WriteLine($"Get 2: {linkedListDoubly1.Get(2)}");
            Console.WriteLine();

            Console.Write($"Pop: {linkedListDoubly1.Pop()} => ");
            linkedListDoubly1.Show();
            Console.WriteLine();

            Console.Write("Insert: ");
            linkedListDoubly1.Insert(3, 2);
            linkedListDoubly1.Insert(2, 1);
            linkedListDoubly1.Insert(1, 0);
            linkedListDoubly1.Show();
            Console.WriteLine();

            Console.Write("Remove: ");
            linkedListDoubly1.Remove(4);
            linkedListDoubly1.Remove(3);
            linkedListDoubly1.Remove(2);
            linkedListDoubly1.Show();
            Console.WriteLine();

            Console.Write("Swap: ");
            linkedListDoubly1.Swap(0, 1);
            linkedListDoubly1.Show();
            Console.WriteLine();

            Console.Write("ToString: ");
            Console.WriteLine(linkedListDoubly1);
            Console.WriteLine();

            linkedListDoubly1.Debug();
            Console.WriteLine();
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