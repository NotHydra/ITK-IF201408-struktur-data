namespace Tree
{
    public class CommandLineInterface()
    {
        private static readonly int borderLength = 120;

        private static readonly string[] menus = ["List", "Add", "Use"];

        private static readonly Type[] structures = [
            typeof(Structure.Tree<>),

        ];
        private static readonly Type[] types = [
            typeof(int),
            typeof(char)
        ];

        private static readonly List<object> containers = [];
        private static readonly string[] blackListedMethods = ["get_First", "set_First", "get_Last", "set_Last", "get_Root", "set_Root", "ToString", "GetType", "Equals", "GetHashCode"];

        private static ConsoleKey pressedKey;

        public static void Start()
        {
            FillContainer();

            int menuOption = 0;
            while (true)
            {
                Console.Clear();
                Border();
                Title("Advance Sorting Manager");
                Border();
                SubTitle("Move = Up/Down | Select = Enter/Right | Exit = Esc/Left");
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

            SubTitle("Thank You For Using Our Program");
            Border();
        }

        private static void FillContainer()
        {
            Structure.Tree<char> tree1 = new();
            tree1.Add('C');
            tree1.Add('H');
            tree1.Add('A');
            tree1.Add('N');
            tree1.Add('D');
            tree1.Add('R');
            tree1.Add('A');
            containers.Add(tree1);
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
                if (blackListedMethods.Contains(method.Name))
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
            text = " " + text + " ";
            int length = (borderLength - text.Length) / 2;

            Console.Write(String.Concat(Enumerable.Repeat("<", ((text.Length % 2) == 0) ? length : (length + 1))));
            Console.Write(text);
            Console.WriteLine(String.Concat(Enumerable.Repeat(">", length)));
        }

        private static void SubTitle(string text)
        {
            text = " " + text + " ";
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
                SubTitle("Result");
                Border();

                for (int containerIndex = 0; containerIndex < containers.Count; containerIndex++)
                {
                    Console.Write($"{containerIndex + 1}. ");
                    WriteLineColored($"( {StructureWithTypeToAlias(containers[containerIndex].GetType(), containers[containerIndex].GetType().GetGenericArguments()[0])} )", ConsoleColor.Green);
                    Console.WriteLine(containers[containerIndex]);
                    Console.WriteLine();
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
                SubTitle("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
                Border();
                Console.WriteLine("Choose a structure:");
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
                        SubTitle("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
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
                            SubTitle("Result");
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
                SubTitle("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
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

                    Console.Write($"{containerIndex + 1}. ");
                    WriteLineColored($"( {StructureWithTypeToAlias(containers[containerIndex].GetType(), containers[containerIndex].GetType().GetGenericArguments()[0])} )", ConsoleColor.Green);
                    Console.WriteLine(containers[containerIndex]);
                    Console.WriteLine();

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
                        SubTitle("Move = Up/Down | Select = Enter/Right | Back = Esc/Left");
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

                            if (actions[actionIndex].GetParameters().Length > 0)
                            {
                                string parameterText = "";
                                for (int parameterIndex = 0; parameterIndex < actions[actionIndex].GetParameters().Length; parameterIndex++)
                                {
                                    parameterText += $"{actions[actionIndex].GetParameters()[parameterIndex].Name}: {TypeToAlias(actions[actionIndex].GetParameters()[parameterIndex].ParameterType)}";

                                    if (parameterIndex < (actions[actionIndex].GetParameters().Length - 1))
                                    {
                                        parameterText += ", ";
                                    };
                                };

                                Console.WriteLine($"{actions[actionIndex].Name} ({parameterText})");
                            }
                            else
                            {
                                Console.WriteLine(actions[actionIndex].Name);
                            }


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
                            if (actions[actionOption].Name == "Clear")
                            {
                                SubTitle("Result");
                                Border();

                                try
                                {
                                    actions[actionOption].Invoke(containers[containerOption], null);

                                    WriteLineColored("Success", ConsoleColor.Green);
                                }
                                catch (Exception e)
                                {
                                    WriteLineColored(e.InnerException!.Message, ConsoleColor.Red);
                                };
                            }
                            else if (actions[actionOption].GetParameters().Length == 0)
                            {
                                SubTitle("Result");
                                Border();

                                try
                                {
                                    var timer = System.Diagnostics.Stopwatch.StartNew();

                                    if (actions[actionOption].ReturnType == typeof(void))
                                    {
                                        actions[actionOption].Invoke(containers[containerOption], null);
                                    }
                                    else
                                    {
                                        Console.WriteLine(actions[actionOption].Invoke(containers[containerOption], null));
                                    };

                                    timer.Stop();

                                    Border();

                                    WriteLineColored($"Time elapsed: {timer.ElapsedMilliseconds} ms", ConsoleColor.Green);
                                    WriteLineColored(timer.Elapsed.ToString(), ConsoleColor.Green);
                                }
                                catch (Exception e)
                                {
                                    WriteLineColored(e.InnerException!.Message, ConsoleColor.Red);
                                };
                            }
                            else if (actions[actionOption].GetParameters().Length > 0)
                            {
                                SubTitle("Input");
                                Border();

                                bool inputSuccess = true;
                                List<object> arguments = [];
                                foreach (System.Reflection.ParameterInfo parameter in actions[actionOption].GetParameters())
                                {
                                    Console.WriteLine($"{parameter.Name} ({TypeToAlias(parameter.ParameterType)}): ");
                                    Border();

                                    try
                                    {
                                        arguments.Add(Convert.ChangeType(Console.ReadLine(), parameter.ParameterType)!);
                                        Border();
                                    }
                                    catch (Exception)
                                    {
                                        Border();
                                        WriteLineColored("Invalid input", ConsoleColor.Red);

                                        inputSuccess = false;
                                        break;
                                    };
                                };

                                if (inputSuccess)
                                {
                                    SubTitle("Result");
                                    Border();

                                    try
                                    {
                                        if (actions[actionOption].ReturnType == typeof(void))
                                        {
                                            actions[actionOption].Invoke(containers[containerOption], [.. arguments]);
                                            WriteLineColored("Success", ConsoleColor.Green);
                                        }
                                        else
                                        {
                                            Console.WriteLine(actions[actionOption].Invoke(containers[containerOption], [.. arguments]));
                                        };

                                    }
                                    catch (Exception e)
                                    {
                                        WriteLineColored(e.InnerException!.Message, ConsoleColor.Red);
                                    };
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
            // Test.Tree();
        }
    }
}