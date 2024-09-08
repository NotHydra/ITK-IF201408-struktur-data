namespace StackAndQueue
{
    public class CommandLineInterface()
    {
        private static readonly int borderLength = 60;

        private static readonly string[] menus = ["List", "Add", "Use"];

        private static readonly Type[] structures = [
            typeof(Structures.LinkedListSingly<>),
            typeof(Structures.LinkedListDoubly<>),
            typeof(Structures.Stack<>)
        ];
        private static readonly Type[] types = [
            typeof(int),
            typeof(char),
            typeof(string)
        ];

        private static readonly List<object> containers = [];

        private static ConsoleKey pressedKey;

        public static void Start()
        {
            FillContainer();

            int menuOption = 0;
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

                for (int menuIndex = 0; menuIndex < menus.Length; menuIndex++)
                {
                    if (menuIndex == menuOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    };

                    Console.WriteLine(menus[menuIndex]);

                    if (menuIndex == menuOption)
                    {
                        Console.ResetColor();
                    };
                };

                Border();

                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow)
                {
                    menuOption = (menuOption == 0) ? (menus.Length - 1) : (menuOption - 1);
                }
                else if (pressedKey == ConsoleKey.DownArrow)
                {
                    menuOption = (menuOption == (menus.Length - 1)) ? 0 : (menuOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                {
                    if (menuOption == 0)
                    {
                        HandleListOption();
                    }
                    else if (menuOption == 1)
                    {
                        HandleAddOption();
                    }
                    else if (menuOption == 2)
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
                Console.WriteLine("Result:");
                Border();

                for (int containerIndex = 0; containerIndex < containers.Count; containerIndex++)
                {
                    Console.Write($"{containerIndex + 1}. {containers[containerIndex]} ");
                    WriteLineColored($"( {StructureWithTypeToAlias(containers[containerIndex].GetType(), containers[containerIndex].GetType().GetGenericArguments()[0])} )", ConsoleColor.Green);
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
            int structureOption = 0;
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

                for (int i = 0; i < structures.Length; i++)
                {
                    if (i == structureOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    };

                    Console.WriteLine(StructureToAlias(structures[i]));

                    if (i == structureOption)
                    {
                        Console.ResetColor();
                    };
                };

                Border();

                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow)
                {
                    structureOption = (structureOption == 0) ? (structures.Length - 1) : (structureOption - 1);
                }
                else if (pressedKey == ConsoleKey.DownArrow)
                {
                    structureOption = (structureOption == (structures.Length - 1)) ? 0 : (structureOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                {
                    int typeOption = 0;
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

                        for (int i = 0; i < types.Length; i++)
                        {
                            if (i == typeOption)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("> ");
                            }
                            else
                            {
                                Console.Write("  ");
                            };

                            Console.WriteLine(TypeToAlias(types[i]));

                            if (i == typeOption)
                            {
                                Console.ResetColor();
                            };
                        };

                        Border();

                        pressedKey = Console.ReadKey(true).Key;
                        if (pressedKey == ConsoleKey.UpArrow)
                        {
                            typeOption = (typeOption == 0) ? (types.Length - 1) : (typeOption - 1);
                        }
                        else if (pressedKey == ConsoleKey.DownArrow)
                        {
                            typeOption = (typeOption == (types.Length - 1)) ? 0 : (typeOption + 1);
                        }
                        else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                        {
                            Console.WriteLine("Result:");
                            Border();

                            containers.Add(Activator.CreateInstance(structures[structureOption].MakeGenericType(types[typeOption]))!);

                            Console.WriteLine($"{StructureWithTypeToAlias(structures[structureOption], types[typeOption])} added");
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
            int containerOption = 0;
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

                for (int containerIndex = 0; containerIndex < containers.Count; containerIndex++)
                {
                    if (containerIndex == containerOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    };

                    Console.Write($"{containers[containerIndex]} ");
                    WriteLineColored($"( {StructureWithTypeToAlias(containers[containerIndex].GetType(), containers[containerIndex].GetType().GetGenericArguments()[0])} )", ConsoleColor.Green);

                    if (containerIndex == containerOption)
                    {
                        Console.ResetColor();
                    };
                };

                Border();

                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow)
                {
                    containerOption = (containerOption == 0) ? (containers.Count - 1) : (containerOption - 1);
                }
                else if (pressedKey == ConsoleKey.DownArrow)
                {
                    containerOption = (containerOption == (containers.Count - 1)) ? 0 : (containerOption + 1);
                }
                else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                {
                    int actionOption = 0;
                    List<System.Reflection.MethodInfo> actions = GetMethodsFiltered(containers[containerOption].GetType());
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

                        for (int actionIndex = 0; actionIndex < actions.Count; actionIndex++)
                        {
                            if (actionIndex == actionOption)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("> ");
                            }
                            else
                            {
                                Console.Write("  ");
                            };

                            Console.WriteLine(actions[actionIndex].Name);

                            if (actionIndex == actionOption)
                            {
                                Console.ResetColor();
                            };
                        };

                        Border();

                        pressedKey = Console.ReadKey(true).Key;
                        if (pressedKey == ConsoleKey.UpArrow)
                        {
                            actionOption = (actionOption == 0) ? (actions.Count - 1) : (actionOption - 1);
                        }
                        else if (pressedKey == ConsoleKey.DownArrow)
                        {
                            actionOption = (actionOption == (actions.Count - 1)) ? 0 : (actionOption + 1);
                        }
                        else if (pressedKey == ConsoleKey.Enter || pressedKey == ConsoleKey.RightArrow)
                        {
                            Console.WriteLine("Result:");
                            Border();

                            if (actions[actionOption].Name == "Clear")
                            {
                                try
                                {
                                    actions[actionOption].Invoke(containers[containerOption], null);

                                    Console.WriteLine("Success");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.InnerException!.Message);
                                };
                            }
                            else if ((actions[actionOption].GetParameters().Length == 0) && (actions[actionOption].ReturnType == typeof(void)))
                            {
                                try
                                {
                                    actions[actionOption].Invoke(containers[containerOption], null);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.InnerException!.Message);
                                };
                            }
                            else if ((actions[actionOption].GetParameters().Length == 0) && (actions[actionOption].ReturnType != typeof(void)))
                            {
                                try
                                {
                                    Console.WriteLine(actions[actionOption].Invoke(containers[containerOption], null));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.InnerException!.Message);
                                };
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

            // Test.LinkedListSingly();
            // Test.LinkedListDoubly();
            // Test.Stack();
        }
    }
}