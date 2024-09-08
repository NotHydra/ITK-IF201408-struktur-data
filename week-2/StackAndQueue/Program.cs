namespace StackAndQueue
{
    public class CommandLineInterface()
    {
        private static readonly int borderLength = 60;
        private static readonly string[] menuOptions = ["List", "Add", "Use", "Exit"];
        private static readonly string[] menuAddDataStructureOptions = ["LinkedListSingly", "LinkedListDoubly", "Stack", "Back"];
        private static readonly string[] menuAddDataTypeOptions = ["int", "char", "string", "Back"];
        private static readonly List<object> containers = [];

        private static ConsoleKey pressedKey;

        public static void Start()
        {
            FillContainer();

            int currentMenuOption = 0;
            while (true)
            {
                Console.Clear();
                Border();
                Title("Data Structure Manager");
                Border();
                Console.WriteLine("Choose a menu:");
                Border();

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == currentMenuOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"> {menuOptions[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuOptions[i]}");
                    };
                };

                Border();

                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow)
                {
                    currentMenuOption = (currentMenuOption == 0) ? (menuOptions.Length - 1) : (currentMenuOption - 1);
                }
                else if (pressedKey == ConsoleKey.DownArrow)
                {
                    currentMenuOption = (currentMenuOption == menuOptions.Length - 1) ? 0 : (currentMenuOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter)
                {
                    if (currentMenuOption == 0)
                    {
                        HandleListOption();
                    }
                    else if (currentMenuOption == 1)
                    {
                        HandleAddOption();
                    }
                    else if (currentMenuOption == 2)
                    {
                        // HandleMenuUse();
                    }
                    else if (currentMenuOption == 3)
                    {
                        break;
                    };
                }
                else if (pressedKey == ConsoleKey.Escape)
                {
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

        private static void HandleListOption()
        {
            while (true)
            {
                Console.Clear();
                Border();
                Title("List Menu");
                Border();

                for (int i = 0; i < containers.Count; i++)
                {
                    Console.Write($"{i + 1}. {containers[i]} ");
                    if (containers[i] is Structures.LinkedListSingly<int>)
                    {
                        Console.WriteLine("( LinkedListSingly<int> )");
                    }
                    else if (containers[i] is Structures.LinkedListSingly<char>)
                    {
                        Console.WriteLine("( LinkedListSingly<char> )");
                    }
                    else if (containers[i] is Structures.LinkedListSingly<string>)
                    {
                        Console.WriteLine("( LinkedListSingly<string> )");
                    }
                    else if (containers[i] is Structures.LinkedListDoubly<int>)
                    {
                        Console.WriteLine("( LinkedListDoubly<int> )");
                    }
                    else if (containers[i] is Structures.LinkedListDoubly<char>)
                    {
                        Console.WriteLine("( LinkedListDoubly<char> )");
                    }
                    else if (containers[i] is Structures.LinkedListDoubly<string>)
                    {
                        Console.WriteLine("( LinkedListDoubly<string> )");
                    }
                    else if (containers[i] is Structures.Stack<int>)
                    {
                        Console.WriteLine("( Stack<int> )");
                    }
                    else if (containers[i] is Structures.Stack<char>)
                    {
                        Console.WriteLine("( Stack<char> )");
                    }
                    else if (containers[i] is Structures.Stack<string>)
                    {
                        Console.WriteLine("( Stack<string> )");
                    };
                };

                Border();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("> Back (press any key to continue)");
                Console.ResetColor();
                Border();

                Console.ReadKey(true);
                break;
            };
        }

        private static void HandleAddOption()
        {
            bool isRunning = true;
            int currentAddDataStructureOption = 0;
            while (isRunning)
            {
                Console.Clear();
                Border();
                Title("Add Menu");
                Border();
                Console.WriteLine("Choose a data structure:");
                Border();

                for (int i = 0; i < menuAddDataStructureOptions.Length; i++)
                {
                    if (i == currentAddDataStructureOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"> {menuAddDataStructureOptions[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {menuAddDataStructureOptions[i]}");
                    };
                };

                Border();

                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow)
                {
                    currentAddDataStructureOption = (currentAddDataStructureOption == 0) ? (menuAddDataStructureOptions.Length - 1) : (currentAddDataStructureOption - 1);
                }
                else if (pressedKey == ConsoleKey.DownArrow)
                {
                    currentAddDataStructureOption = (currentAddDataStructureOption == menuAddDataStructureOptions.Length - 1) ? 0 : (currentAddDataStructureOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter)
                {
                    if (currentAddDataStructureOption == (menuAddDataStructureOptions.Length - 1))
                    {
                        break;
                    };

                    int currentAddDataTypeOption = 0;
                    while (true)
                    {
                        Console.Clear();
                        Border();
                        Title("Add Menu");
                        Border();
                        Console.WriteLine("Choose a data type:");
                        Border();

                        for (int i = 0; i < menuAddDataTypeOptions.Length; i++)
                        {
                            if (i == currentAddDataTypeOption)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"> {menuAddDataTypeOptions[i]}");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"  {menuAddDataTypeOptions[i]}");
                            };
                        };

                        Border();

                        pressedKey = Console.ReadKey(true).Key;
                        if (pressedKey == ConsoleKey.UpArrow)
                        {
                            currentAddDataTypeOption = (currentAddDataTypeOption == 0) ? (menuAddDataTypeOptions.Length - 1) : (currentAddDataTypeOption - 1);
                        }
                        else if (pressedKey == ConsoleKey.DownArrow)
                        {
                            currentAddDataTypeOption = (currentAddDataTypeOption == (menuAddDataTypeOptions.Length - 1)) ? 0 : (currentAddDataTypeOption + 1);
                        }
                        else if (pressedKey == ConsoleKey.Enter)
                        {
                            if (currentAddDataTypeOption == (menuAddDataTypeOptions.Length - 1))
                            {
                                break;
                            };

                            if (currentAddDataStructureOption == 0)
                            {
                                Console.Write("LinkedListSingly");
                                if (currentAddDataTypeOption == 0)
                                {
                                    Console.Write("<int>");
                                    Structures.LinkedListSingly<int> linkedListSingly = new();
                                    containers.Add(linkedListSingly);
                                }
                                else if (currentAddDataTypeOption == 1)
                                {
                                    Console.Write("<char>");
                                    Structures.LinkedListSingly<char> linkedListSingly = new();
                                    containers.Add(linkedListSingly);
                                }
                                else if (currentAddDataTypeOption == 2)
                                {
                                    Console.Write("<string>");
                                    Structures.LinkedListSingly<string> linkedListSingly = new();
                                    containers.Add(linkedListSingly);
                                };
                            }
                            else if (currentAddDataStructureOption == 1)
                            {
                                Console.Write("LinkedListDoubly");
                                if (currentAddDataTypeOption == 0)
                                {
                                    Console.Write("<int>");
                                    Structures.LinkedListDoubly<int> linkedListDoubly = new();
                                    containers.Add(linkedListDoubly);
                                }
                                else if (currentAddDataTypeOption == 1)
                                {
                                    Console.Write("<char>");
                                    Structures.LinkedListDoubly<char> linkedListDoubly = new();
                                    containers.Add(linkedListDoubly);
                                }
                                else if (currentAddDataTypeOption == 2)
                                {
                                    Console.Write("<string>");
                                    Structures.LinkedListDoubly<string> linkedListDoubly = new();
                                    containers.Add(linkedListDoubly);
                                };
                            }
                            else if (currentAddDataStructureOption == 2)
                            {
                                Console.Write("Stack");
                                if (currentAddDataTypeOption == 0)
                                {
                                    Console.Write("<int>");
                                    Structures.Stack<int> stack = new();
                                    containers.Add(stack);
                                }
                                else if (currentAddDataTypeOption == 1)
                                {
                                    Console.Write("<char>");
                                    Structures.Stack<char> stack = new();
                                    containers.Add(stack);
                                }
                                else if (currentAddDataTypeOption == 2)
                                {
                                    Console.Write("<string>");
                                    Structures.Stack<string> stack = new();
                                    containers.Add(stack);
                                };
                            };

                            Console.WriteLine(" Added");
                            Border();

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("> Back (press any key to continue)");
                            Console.ResetColor();
                            Border();

                            Console.ReadKey(true);
                            isRunning = false;
                            break;
                        }
                        else if (pressedKey == ConsoleKey.Escape)
                        {
                            break;
                        };
                    };
                }
                else if (pressedKey == ConsoleKey.Escape)
                {
                    break;
                };

                if (!isRunning)
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