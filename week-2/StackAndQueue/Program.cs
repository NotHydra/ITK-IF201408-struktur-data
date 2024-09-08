namespace StackAndQueue
{
    public class CommandLineInterface()
    {
        private static readonly int borderLength = 60;

        private static readonly string[] menuOptions = ["List", "Add", "Use"];

        private static readonly Type[] menuAddDataStructureOptions = [
            typeof(Structures.LinkedListSingly<>),
            typeof(Structures.LinkedListDoubly<>),
            typeof(Structures.Stack<>)
        ];
        private static readonly Type[] menuAddDataTypeOptions = [
            typeof(int),
            typeof(char),
            typeof(string)
        ];

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
                Title("Move = Up/Down | Select = Enter/Right | Exit = Esc/Left");
                Border();
                Console.WriteLine("Choose a menu:");
                Border();

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == currentMenuOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    };

                    Console.WriteLine(menuOptions[i]);

                    if (i == currentMenuOption)
                    {
                        Console.ResetColor();
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
                    currentMenuOption = (currentMenuOption == (menuOptions.Length - 1)) ? 0 : (currentMenuOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
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
                        HandleUseOption();
                    };
                }
                else if (pressedKey == ConsoleKey.Escape || pressedKey == ConsoleKey.LeftArrow)
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

        private static string TypeToAlias(Type type)
        {
            if (type == typeof(int))
            {
                return "int";
            }

            if (type == typeof(char))
            {
                return "char";
            }

            if (type == typeof(string))
            {
                return "string";
            }

            return "object";
        }

        private static string StructureToAlias(Type structure)
        {
            return structure.Name.Split('`')[0];
        }

        private static string StructureWithTypeToAlias(Type structure, Type type)
        {
            return StructureToAlias(structure) + "<" + TypeToAlias(type) + ">";
        }

        private static List<System.Reflection.MethodInfo> GetMethodsFiltered(Type type)
        {
            List<System.Reflection.MethodInfo> filteredMethods = [];
            foreach (System.Reflection.MethodInfo method in type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (method.Name == "get_First")
                {
                    continue;
                };

                if (method.Name == "set_First")
                {
                    continue;
                };

                if (method.Name == "get_Last")
                {
                    continue;
                };

                if (method.Name == "set_Last")
                {
                    continue;
                };

                if (method.Name == "ToString")
                {
                    continue;
                };

                if (method.Name == "GetType")
                {
                    continue;
                };

                if (method.Name == "Equals")
                {
                    continue;
                };

                if (method.Name == "GetHashCode")
                {
                    continue;
                };

                filteredMethods.Add(method);
            };

            return filteredMethods;
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

        private static void WriteLineColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
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
                    WriteLineColored($"( {StructureWithTypeToAlias(containers[i].GetType(), containers[i].GetType().GetGenericArguments()[0])} )", ConsoleColor.Green);
                };

                Border();

                WriteLineColored("> Back (press any key to continue)", ConsoleColor.Cyan);
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
                Title("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
                Border();
                Console.WriteLine("Choose a data structure:");
                Border();

                for (int i = 0; i < menuAddDataStructureOptions.Length; i++)
                {
                    if (i == currentAddDataStructureOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    };

                    Console.WriteLine(StructureToAlias(menuAddDataStructureOptions[i]));

                    if (i == currentAddDataStructureOption)
                    {
                        Console.ResetColor();
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
                    currentAddDataStructureOption = (currentAddDataStructureOption == (menuAddDataStructureOptions.Length - 1)) ? 0 : (currentAddDataStructureOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                {
                    int currentAddDataTypeOption = 0;
                    while (true)
                    {
                        Console.Clear();
                        Border();
                        Title("Add Menu");
                        Border();
                        Title("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
                        Border();
                        Console.WriteLine("Choose a data type:");
                        Border();

                        for (int i = 0; i < menuAddDataTypeOptions.Length; i++)
                        {
                            if (i == currentAddDataTypeOption)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("> ");
                            }
                            else
                            {
                                Console.Write("  ");
                            };

                            Console.WriteLine(TypeToAlias(menuAddDataTypeOptions[i]));

                            if (i == currentAddDataTypeOption)
                            {
                                Console.ResetColor();
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
                        else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                        {
                            containers.Add(Activator.CreateInstance(menuAddDataStructureOptions[currentAddDataStructureOption].MakeGenericType(menuAddDataTypeOptions[currentAddDataTypeOption]))!);

                            Console.WriteLine($"{StructureWithTypeToAlias(menuAddDataStructureOptions[currentAddDataStructureOption], menuAddDataTypeOptions[currentAddDataTypeOption])} added");
                            Border();

                            WriteLineColored("> Back (press any key to continue)", ConsoleColor.Cyan);
                            Border();

                            Console.ReadKey(true);
                            isRunning = false;
                            break;
                        }
                        else if (pressedKey == ConsoleKey.Escape || pressedKey == ConsoleKey.LeftArrow)
                        {
                            break;
                        };
                    };
                }
                else if (pressedKey == ConsoleKey.Escape || pressedKey == ConsoleKey.LeftArrow)
                {
                    break;
                };

                if (!isRunning)
                {
                    break;
                };
            };
        }

        private static void HandleUseOption()
        {
            int currentUseContainerOption = 0;
            while (true)
            {
                Console.Clear();
                Border();
                Title("Use Menu");
                Border();
                Title("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
                Border();
                Console.WriteLine("Choose a container:");
                Border();

                for (int i = 0; i < containers.Count; i++)
                {
                    if (i == currentUseContainerOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    };

                    Console.Write($"{containers[i]} ");
                    WriteLineColored($"( {StructureWithTypeToAlias(containers[i].GetType(), containers[i].GetType().GetGenericArguments()[0])} )", ConsoleColor.Green);

                    if (i == currentUseContainerOption)
                    {
                        Console.ResetColor();
                    };
                };

                Border();

                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow)
                {
                    currentUseContainerOption = (currentUseContainerOption == 0) ? (containers.Count - 1) : (currentUseContainerOption - 1);
                }
                else if (pressedKey == ConsoleKey.DownArrow)
                {
                    currentUseContainerOption = (currentUseContainerOption == (containers.Count - 1)) ? 0 : (currentUseContainerOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                {
                    int currentUseActionOption = 0;
                    List<System.Reflection.MethodInfo> actions = GetMethodsFiltered(containers[currentUseContainerOption].GetType());
                    while (true)
                    {
                        Console.Clear();
                        Border();
                        Title("Use Menu");
                        Border();
                        Title("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
                        Border();
                        Console.WriteLine("Choose an action:");
                        Border();

                        for (int i = 0; i < actions.Count; i++)
                        {
                            if (i == currentUseActionOption)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("> ");
                            }
                            else
                            {
                                Console.Write("  ");
                            };

                            Console.WriteLine(actions[i].Name);

                            if (i == currentUseActionOption)
                            {
                                Console.ResetColor();
                            };
                        };

                        Border();

                        pressedKey = Console.ReadKey(true).Key;
                        if (pressedKey == ConsoleKey.UpArrow)
                        {
                            currentUseActionOption = (currentUseActionOption == 0) ? (actions.Count - 1) : (currentUseActionOption - 1);
                        }
                        else if (pressedKey == ConsoleKey.DownArrow)
                        {
                            currentUseActionOption = (currentUseActionOption == (actions.Count - 1)) ? 0 : (currentUseActionOption + 1);
                        }
                        else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                        {
                            if (actions[currentUseActionOption].GetParameters().Length == 0)
                            {
                                actions[currentUseActionOption].Invoke(containers[currentUseContainerOption], null);
                            }
                            else
                            {
                                throw new NotImplementedException();
                            };

                            Border();

                            WriteLineColored("> Choose action (press any key)", ConsoleColor.Cyan);
                            Border();

                            pressedKey = Console.ReadKey(true).Key;
                            if (pressedKey == ConsoleKey.Escape || pressedKey == ConsoleKey.LeftArrow)
                            {
                                break;
                            };
                        }
                        else if (pressedKey == ConsoleKey.Escape || pressedKey == ConsoleKey.LeftArrow)
                        {
                            break;
                        };
                    };
                }
                else if (pressedKey == ConsoleKey.Escape || pressedKey == ConsoleKey.LeftArrow)
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
            // TestLinkedListSingly();
            // TestLinkedListDoubly();
            // TestStack();
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